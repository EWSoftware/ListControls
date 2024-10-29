//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ExtendedTreeView.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/29/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains an extended tree view control that is fully owner-drawn to overcome some limitations in
// the way the default tree view is drawn.  It also provides several additional features to make it easier to
// use than the standard tree view control.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/09/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is an extended tree view control that is fully owner-drawn to overcome some limitations in the way
    /// the default tree view is drawn.  It also provides several additional features to make it easier to use
    /// than the standard tree view control.
    /// </summary>
    [Description("An extended tree view control with several extra features")]
    public class ExtendedTreeView : TreeView, IEnumerable, IEnumerable<TreeNode>
    {
        #region Private data members
        //=====================================================================

        // Windows messages
        private const int WmEraseBackground = 0x0014;
        private const int WmKeyDown = 0x0100;
        private const int WmLButtonDoubleClick = 0x0203;
        private const int WmThemeChanged = 0x031A;

        // The path to the embedded resources
        private const string ResourcePath = "EWSoftware.ListControls.Images.";

        // The internal image lists
        private static readonly ImageList ilPlusMinus, ilCheckbox;

        // Instance expando image list
        private ImageList ilExpando;

        // State information
        private int idxCollapse, idxExpand, idxChecked, idxUnchecked, idxMixed;
        private bool drawDefaultImages, useThemedImages, selectOnROLClick, allowCollapse,
            syncParentChildCheckedState, checkingParent;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to set or get whether or not to use the themed versions of the expando and checkbox
        /// images even when themes are not being used.
        /// </summary>
        /// <value>The default is false and the images will be based on the current visual style setting</value>
        [Category("Appearance"), DefaultValue(false), Description("Indicate whether or not to always use the " +
          "themed versions of the expando and checkbox images even when themes are not being used")]
        public bool UseThemedImages
        {
            get => useThemedImages;
            set
            {
                useThemedImages = value;

                this.SetImages();
                this.Invalidate();
                this.Update();
            }
        }

        /// <summary>
        /// This property is used to set or get the expando image list used to show the +/- images when the
        /// <see cref="TreeView.ShowPlusMinus"/> property is set to true.
        /// </summary>
        /// <value><para>The default is null to use the standard images.  If set, the image list should contain
        /// images in the following order:</para>
        /// 
        /// <list type="table">
        ///    <listheader>
        ///       <term>Image Index</term>
        ///       <description>Description</description>
        ///    </listheader>
        ///    <item>
        ///       <term>0</term>
        ///       <description>Unthemed collapse image (required)</description>
        ///    </item>
        ///    <item>
        ///       <term>1</term>
        ///       <description>Unthemed expand image (required)</description>
        ///    </item>
        ///    <item>
        ///       <term>2</term>
        ///       <description>Themed collapse image (optional)</description>
        ///    </item>
        ///    <item>
        ///       <term>3</term>
        ///       <description>Themed expand image (optional)</description>
        ///    </item>
        /// </list>
        /// 
        /// <para>If you do not need separate themed and unthemed versions, you may omit the last two entries.</para>
        /// </value>
        [Category("Appearance"), DefaultValue(null),
          Description("Specify a custom image list for the expando (+/-) images")]
        public ImageList ExpandoImageList
        {
            get => ilExpando;
            set
            {
                if(ilExpando != value)
                {
                    if(ilExpando != null)
                    {
                        ilExpando.RecreateHandle -= this.ImageList_RecreateHandle;
                        ilExpando.Disposed -= this.ImageList_Disposed;
                    }

                    ilExpando = value;

                    if(value != null)
                    {
                        ilExpando.RecreateHandle += this.ImageList_RecreateHandle;
                        ilExpando.Disposed += this.ImageList_Disposed;
                    }

                    this.SetImages();
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not nodes can be collapsed when
        /// <see cref="TreeView.ShowPlusMinus"/> is set to false.
        /// </summary>
        /// <value>The default is true to match the standard tree view's behavior</value>
        [Category("Behavior"), DefaultValue(true), Description("Specify whether or not nodes can be collapsed " +
          "when ShowPlusMinus is set to false")]
        public bool AllowCollapse
        {
            get => allowCollapse;
            set => allowCollapse = value;
        }

        /// <summary>
        /// This property is used to set or get whether or not the tree view will draw default images for tree
        /// nodes without images.
        /// </summary>
        /// <value>The default is true to match the standard tree view's behavior</value>
        [Category("Behavior"), DefaultValue(true), Description("Specify whether or not the default tree view " +
          "images are drawn for tree nodes without images")]
        public bool DrawDefaultImages
        {
            get => drawDefaultImages;
            set => drawDefaultImages = value;
        }

        /// <summary>
        /// This property is used to set or get whether or not the tree view will select a node if the blank area
        /// to the right of the label is clicked.
        /// </summary>
        /// <value>The default is false to match the standard tree view's behavior</value>
        [Category("Behavior"), DefaultValue(false), Description("Specify whether or not the tree view selects " +
          "a node when a click occurs in the blank area to the right of the label")]
        public bool SelectOnRightOfLabelClick
        {
            get => selectOnROLClick;
            set => selectOnROLClick = value;
        }

        /// <summary>
        /// This is used to set or get whether or not the parent and child node checked states are synchronized
        /// </summary>
        /// <value>The default is false to match the standard tree view's behavior.  This can be set to true to
        /// ensure that the checked state of parent and child nodes is synchronized.  If a child node is checked,
        /// all parent nodes are marked as checked too.  If a parent is checked or unchecked, all child nodes are
        /// checked or unchecked as well.  The checkbox on parent nodes with a mix of checked and unchecked
        /// children are drawn as a mixed checkbox to make them easy to pick out when collapsed.  This is
        /// accomplished by using the tree node's <see cref="TreeNode.StateImageIndex"/> property.  If you have
        /// specified a custom state image list, the third image is used to represent the mixed state.</value>
        /// <remarks>If you add or remove nodes from the tree after setting this to true, call
        /// <see cref="SynchronizeCheckedStates"/> to refresh the state of the parent and child check states.</remarks>
        [Category("Behavior"), DefaultValue(false), Description("Specify whether or not the parent and child " +
          "checked states are synchronized")]
        public bool SyncParentChildCheckedState
        {
            get => syncParentChildCheckedState;
            set
            {
                syncParentChildCheckedState = value;

                if(base.CheckBoxes && value)
                    this.SynchronizeCheckedStates();
            }
        }

        /// <summary>
        /// This property will return a collection containing the currently checked nodes in the tree view
        /// </summary>
        /// <remarks>The collection is read-only but the items in it are not.</remarks>
        [Category("Data"), Browsable(false), Description("The currently checked items in the tree view")]
        public CheckedNodesCollection CheckedNodes
        {
            get
            {
                List<TreeNode> list = new List<TreeNode>();

                foreach(TreeNode n in this)
                    if(n.Checked)
                        list.Add(n);

                return new CheckedNodesCollection(list);
            }
        }

        /// <summary>
        /// This is reimplemented to set the new default value and to reset <c>OwnerDrawText</c> to
        /// <c>OwnerDrawAll</c>.  It is not available in release builds.
        /// </summary>
        /// <remarks>This control does not support <c>OwnerDrawText</c>.  If specified, it will be converted to
        /// <c>OwnerDrawAll</c>.</remarks>
        /// <exclude/>
        [DefaultValue(TreeViewDrawMode.OwnerDrawAll), RefreshProperties(RefreshProperties.All)
#if !DEBUG
         , Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
#endif
        ]
        public new TreeViewDrawMode DrawMode
        {
            get => base.DrawMode;
            set
            {
                if(value == TreeViewDrawMode.OwnerDrawText)
                    value = TreeViewDrawMode.OwnerDrawAll;

                base.DrawMode = value;
            }
        }

        /// <summary>
        /// This is reimplemented to change the image list based on the current settings
        /// </summary>
        public new bool CheckBoxes
        {
            get => base.CheckBoxes;
            set
            {
                this.BeginUpdate();
                this.SelectedNode = null;

                base.CheckBoxes = value;

                if(!this.DesignMode)
                {
                    if(!value && base.StateImageList == ilCheckbox)
                        base.StateImageList = null;
                    else
                    {
                        if(value && base.StateImageList == null)
                            base.StateImageList = ilCheckbox;
                    }

                    if(value && syncParentChildCheckedState)
                        this.SynchronizeCheckedStates();
                }

                this.EndUpdate();
            }
        }

        /// <summary>
        /// This is reimplemented to change the image list based on the current settings
        /// </summary>
        public new ImageList StateImageList
        {
            get
            {
                if(base.StateImageList == ilCheckbox)
                    return null;

                return base.StateImageList;
            }
            set
            {
                if(value == null && base.CheckBoxes)
                    base.StateImageList = ilCheckbox;
                else
                    base.StateImageList = value;
            }
        }

        /// <summary>
        /// This is reimplemented to change the default line color to match the standard tree view
        /// </summary>
        [DefaultValue(typeof(Color), "Silver")]
        public new Color LineColor
        {
            get => base.LineColor;
            set => base.LineColor = value;
        }

        /// <summary>
        /// This can be used to get a tree node by name from the tree view (a root node or any child at any
        /// level).
        /// </summary>
        /// <param name="name">The name of the tree node to retrieve.</param>
        /// <value>Returns the tree node that was found or null if it was not found.</value>
        /// <remarks>The tree's nodes are searched recursively for the first node with a name that matches the
        /// specified value.  The value is case-sensitive.  Note that node names do not have to be unique.
        /// Only the first node found is returned even if other nodes further down the tree have an identical
        /// name.</remarks>
        /// <example>
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="Tree Node Indexer Example" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="Tree Node Indexer Example" />
        /// </example>
        [Description("Get the specified column from the current row")]
        public TreeNode this[string name]
        {
            get
            {
                foreach(TreeNode n in this)
                    if(n.Name == name)
                        return n;

                return null;
            }
        }
        #endregion

        #region Events
        //=====================================================================

#pragma warning disable 0067
        /// <summary>
        /// This is used to hide the base class's draw node event which is not used by this control
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event DrawTreeNodeEventHandler DrawNode;

#pragma warning restore 0067

        /// <summary>
        /// This event is raised after a click is detected on a node's state image or the space bar is hit on the
        /// selected node.
        /// </summary>
        /// <remarks>This event is only raised if a <see cref="StateImageList" /> has been defined for the tree
        /// view, the <see cref="CheckBoxes"/> property is false, and the node has a
        /// <see cref="TreeNode.StateImageIndex"/> or a <see cref="TreeNode.StateImageKey"/> defined.</remarks>
        [Category("Action"), Description("Occurs after a click is detected on a node's state image or the " +
          "space bar is hit on the selected node.")]
        public event TreeViewEventHandler ChangeStateImage;

        /// <summary>
        /// This raises the <see cref="ChangeStateImage"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnChangeStateImage(TreeViewEventArgs e)
        {
            ChangeStateImage?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised before the node is drawn
        /// </summary>
        /// <remarks>This event can be used to modify the parameters in the event arguments or custom draw parts
        /// of the node prior to the tree view drawing anything.</remarks>
        /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
        /// for an example of custom drawing the tree nodes.</example>
        [Category("Behavior"), Description("Occurs before the node is drawn.")]
        public event EventHandler<DrawTreeNodeExtendedEventArgs> TreeNodeDrawing;

        /// <summary>
        /// This raises the <see cref="TreeNodeDrawing"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
        /// for an example of custom drawing the tree nodes.</example>
        protected virtual void OnTreeNodeDrawing(DrawTreeNodeExtendedEventArgs e)
        {
            TreeNodeDrawing?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised after the node is drawn
        /// </summary>
        /// <remarks>This event can be used to custom draw parts of the node after the tree view has drawn the
        /// node.</remarks>
        /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
        /// for an example of custom drawing the tree nodes.</example>
        [Category("Behavior"), Description("Occurs after the node has been drawn.")]
        public event EventHandler<DrawTreeNodeExtendedEventArgs> TreeNodeDrawn;

        /// <summary>
        /// This raises the <see cref="TreeNodeDrawn"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
        /// for an example of custom drawing the tree nodes.</example>
        protected virtual void OnTreeNodeDrawn(DrawTreeNodeExtendedEventArgs e)
        {
            TreeNodeDrawn?.Invoke(this, e);
        }
        #endregion

        #region Private methods
        //=====================================================================

        /// <summary>
        /// Invalidate the control when the expando image list handle is recreated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_RecreateHandle(object sender, EventArgs e)
        {
            if(this.IsHandleCreated)
                this.Invalidate(true);
        }

        /// <summary>
        /// Detach the expando image list when it is disposed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_Disposed(object sender, EventArgs e)
        {
            this.ExpandoImageList = null;
        }

        /// <summary>
        /// This is used to set the images to use based on the visual style
        /// </summary>
        private void SetImages()
        {
            if(Application.RenderWithVisualStyles || useThemedImages)
            {
                if(ilExpando != null && ilExpando.Images.Count < 4)
                {
                    idxCollapse = 0;
                    idxExpand = 1;
                }
                else
                {
                    idxCollapse = 2;
                    idxExpand = 3;
                }

                idxUnchecked = 3;
                idxChecked = 4;
                idxMixed = 5;
            }
            else
            {
                idxCollapse = idxUnchecked = 0;
                idxExpand = idxChecked = 1;
                idxMixed = 2;
            }
        }

        /// <summary>
        /// Draw the various line parts for the given node
        /// </summary>
        /// <param name="g">The graphics object to use</param>
        /// <param name="pen">The pen to use</param>
        /// <param name="node">The tree node for which to draw lines</param>
        /// <param name="bounds">The bounds off the node</param>
        /// <param name="left">The starting point for the line</param>
        /// <param name="width">The width of the connecting bar</param>
        /// <param name="rootLine">True for a root line, false if not</param>
        private static void DrawLineParts(Graphics g, Pen pen, TreeNode node,
          Rectangle bounds, int left, int width, bool rootLine)
        {
            int height = bounds.Height / 2;

            if(((node.PrevNode != null || node.Parent != null) && node.NextNode != null) || rootLine)
                g.DrawLine(pen, left, bounds.Top, left, bounds.Top + bounds.Height);
            else
            {
                if(node.PrevNode != null || node.Parent != null)
                    g.DrawLine(pen, left, bounds.Top, left, bounds.Top + height + 1);

                if(node.NextNode != null)
                    g.DrawLine(pen, left, bounds.Top + height, left, bounds.Top + bounds.Height);
            }

            if(!rootLine)
                g.DrawLine(pen, left + 1, bounds.Top + height, left + width, bounds.Top + height);
        }

        /// <summary>
        /// Update the parent node state image based on the child's state and its children if any
        /// </summary>
        /// <param name="child">The child node</param>
        private static void UpdateParentImageState(TreeNode child)
        {
            TreeNode parent = child.Parent;

            if(parent == null)
                return;

            if(child.StateImageIndex == (int)NodeCheckState.Mixed)
                parent.StateImageIndex = (int)NodeCheckState.Mixed;
            else if(ExtendedTreeView.AllChildrenChecked(parent))
                parent.StateImageIndex = (int)NodeCheckState.Checked;
            else if(ExtendedTreeView.AllChildrenUnchecked(parent))
                parent.StateImageIndex = (int)NodeCheckState.Unchecked;
            else
                parent.StateImageIndex = (int)NodeCheckState.Mixed;

            UpdateParentImageState(parent);
        }
        #endregion

        #region IEnumerable Members
        //=====================================================================

        /// <summary>
        /// Return a <see cref="TreeNodeEnumerator"/> to enumerate all nodes in the tree recursively
        /// </summary>
        /// <returns>The enumerator to use</returns>
        /// <example>
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="C# - Enumerate the entire tree" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="VB.NET - Enumerate the entire tree" />
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="TreeNodeEnumerator Example"
        ///   title="C# - Enumerate starting at a selected node" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="TreeNodeEnumerator Example"
        ///   title="VB.NET - Enumerate starting at a selected node" />
        /// </example>
        public IEnumerator GetEnumerator()
        {
            TreeNode start = (this.Nodes.Count == 0) ? null : this.Nodes[0];

            return new TreeNodeEnumerator(start, true);
        }
        #endregion

        #region IEnumerable<TreeNode> Members
        //=====================================================================

        /// <summary>
        /// Return a <see cref="TreeNodeEnumerator"/> to enumerate all nodes in the tree recursively
        /// </summary>
        /// <returns>The an enumerable list of tree nodes</returns>
        /// <example>
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="C# - Enumerate the entire tree" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="VB.NET - Enumerate the entire tree" />
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="TreeNodeEnumerator Example"
        ///   title="C# - Enumerate starting at a selected node" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="TreeNodeEnumerator Example"
        ///   title="VB.NET - Enumerate starting at a selected node" />
        /// </example>
        IEnumerator<TreeNode> IEnumerable<TreeNode>.GetEnumerator()
        {
            foreach(TreeNode n in this)
                yield return n;
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtendedTreeView() : base()
        {
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.LineColor = Color.Silver;

            drawDefaultImages = allowCollapse = true;

            this.SetImages();
        }

        /// <summary>
        /// Static constructor.  This loads the images.
        /// </summary>
        static ExtendedTreeView()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            ilPlusMinus = new ImageList { ImageSize = new Size(9, 9) };
            ilCheckbox = new ImageList { ImageSize = new Size(13, 13) };

            ilPlusMinus.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "UnthemedCollapse.bmp")), Color.Magenta);
            ilPlusMinus.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "UnthemedExpand.bmp")), Color.Magenta);
            ilPlusMinus.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "ThemedCollapse.bmp")), Color.Magenta);
            ilPlusMinus.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "ThemedExpand.bmp")), Color.Magenta);

            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "UnthemedUnchecked.bmp")), Color.Magenta);
            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "UnthemedChecked.bmp")), Color.Magenta);
            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "UnthemedMixed.bmp")), Color.Magenta);
            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "ThemedUnchecked.bmp")), Color.Magenta);
            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "ThemedChecked.bmp")), Color.Magenta);
            ilCheckbox.Images.Add(new Bitmap(asm.GetManifestResourceStream(
                ResourcePath + "ThemedMixed.bmp")), Color.Magenta);
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// This is overridden to update the images when the theme changes
        /// </summary>
        /// <param name="m">The message to process</param>
        protected override void WndProc(ref Message m)
        {
            // Ignore WM_ERASEBACKGROUND to reduce flickering
            if(m.Msg == ExtendedTreeView.WmEraseBackground)
                return;

            // Update the images used on theme changes
            if(m.Msg == ExtendedTreeView.WmThemeChanged)
                this.SetImages();
            else
            {
                if(m.Msg == ExtendedTreeView.WmLButtonDoubleClick && this.CheckBoxes)
                {
                    // Filter WM_LBUTTONDBLCLK when we're showing check boxes.  This fixes a bug that causes an
                    // inconsistent checked state when double clicking on checkboxes in the tree view.

                    // See if we're over the checkbox.  If so then we'll handle the toggling of it ourselves.
                    TreeViewHitTestInfo hitTestInfo = HitTest(m.LParam.ToInt32() & 0xFFFF,
                        (m.LParam.ToInt32() >> 16) & 0xFFFF);

                    if(hitTestInfo.Node != null && hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                    {
                        this.OnBeforeCheck(new TreeViewCancelEventArgs(hitTestInfo.Node, false, TreeViewAction.ByMouse));

                        hitTestInfo.Node.Checked = !hitTestInfo.Node.Checked;

                        this.OnAfterCheck(new TreeViewEventArgs(hitTestInfo.Node, TreeViewAction.ByMouse));

                        m.Result = IntPtr.Zero;
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// This is overridden to handle the various extra keys recognized by this control
        /// </summary>
        /// <param name="msg">The command key message</param>
        /// <param name="keyData">The key to process</param>
        /// <returns>True if the key was handled, false if not.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            TreeNode node;
            Keys key;
            bool keyHandled, ctrlPressed, shiftPressed;

            if(msg.Msg == ExtendedTreeView.WmKeyDown)
            {
                key = keyData & Keys.KeyCode;
                ctrlPressed = (keyData & Keys.Control) != Keys.None;
                shiftPressed = (keyData & Keys.Shift) != Keys.None;
                keyHandled = false;

                switch(key)
                {
                    case Keys.Space:
                        if(!base.CheckBoxes && base.StateImageList != null && base.SelectedNode != null &&
                          base.SelectedNode.StateImageIndex != -1)
                        {
                            OnChangeStateImage(new TreeViewEventArgs(base.SelectedNode, TreeViewAction.ByKeyboard));
                            keyHandled = true;
                        }
                        break;

                    case Keys.F2:
                        if(base.LabelEdit && base.SelectedNode != null)
                        {
                            base.SelectedNode.BeginEdit();
                            keyHandled = true;
                        }
                        break;

                    case Keys.K:
                        if(ctrlPressed && (base.ShowPlusMinus || allowCollapse))
                        {
                            node = base.SelectedNode;

                            if(shiftPressed)
                            {
                                base.CollapseAll();

                                // Reselect the last node if it is a parent node
                                if(node != null && node.Parent == null)
                                {
                                    base.SelectedNode = node;
                                    node.EnsureVisible();
                                }
                            }
                            else
                                node?.Collapse();

                            keyHandled = true;
                        }
                        break;

                    case Keys.E:
                        if(ctrlPressed)
                        {
                            node = base.SelectedNode;

                            if(shiftPressed)
                                base.ExpandAll();
                            else
                                node?.ExpandAll();

                            keyHandled = true;

                            node?.EnsureVisible();
                        }
                        break;

                    default:
                        break;
                }

                if(keyHandled == true)
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// This is overridden to draw a tree node
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>Note that the <see cref="DrawNode"/> event is not raised.  The <see cref="TreeNodeDrawing"/>
        /// and <see cref="TreeNodeDrawn"/> events are raised instead.</remarks>
        /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
        /// for an example of custom drawing the tree nodes.</example>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            Graphics g = e.Graphics;
            Rectangle bounds = e.Bounds;
            TreeNode parent, node = e.Node;
            ImageList ilCheckState = null, ilPM = null;
            DrawTreeNodeExtendedEventArgs extArgs = null;

            Color lineColor, bgColor, fgColor, bgTextColor = Color.Empty;

            int height, imgHeight, width, top, left, lineLevel, indent = base.Indent, level = node.Level;

            // Ignore if there is nothing to draw or if disposing
            if(bounds.IsEmpty || node.TreeView.Nodes.Count == 0)
                return;

            try
            {
                extArgs = new DrawTreeNodeExtendedEventArgs(g, node, e.State, e.Bounds);

                #region Determine the images

                // Determine the images
                if(base.ImageList != null)
                {
                    imgHeight = base.ImageList.ImageSize.Height;

                    string imageKey;

                    if(base.SelectedNode == node)
                    {
                        extArgs.ImageIndex = node.SelectedImageIndex;
                        imageKey = node.SelectedImageKey;

                        if(extArgs.ImageIndex == -1 && imageKey.Length == 0 && drawDefaultImages)
                        {
                            extArgs.ImageIndex = base.SelectedImageIndex;
                            imageKey = base.SelectedImageKey;
                        }
                    }
                    else
                    {
                        extArgs.ImageIndex = node.ImageIndex;
                        imageKey = node.ImageKey;

                        if(extArgs.ImageIndex == -1 && imageKey.Length == 0 && drawDefaultImages)
                        {
                            extArgs.ImageIndex = base.ImageIndex;
                            imageKey = base.ImageKey;
                        }
                    }

                    if(imageKey.Length != 0)
                        extArgs.ImageIndex = base.ImageList.Images.IndexOfKey(imageKey);

                    if(extArgs.ImageIndex != -1)
                        extArgs.NodeParts |= NodeParts.NodeImage;
                }
                else
                    imgHeight = 16;

                if(base.StateImageList != null && base.StateImageList != ilCheckbox)
                {
                    ilCheckState = base.StateImageList;

                    if(base.CheckBoxes)
                    {
                        if(!syncParentChildCheckedState)
                            extArgs.StateImageIndex = (int)(node.Checked ? NodeCheckState.Checked : NodeCheckState.Unchecked);
                        else
                            extArgs.StateImageIndex = node.StateImageIndex;
                    }
                    else
                    {
                        if(node.StateImageKey.Length != 0)
                            extArgs.StateImageIndex = ilCheckState.Images.IndexOfKey(node.StateImageKey);
                        else
                            extArgs.StateImageIndex = node.StateImageIndex;
                    }
                }
                else
                {
                    if(base.CheckBoxes)
                    {
                        ilCheckState = ilCheckbox;
                        extArgs.StateImageIndex = (node.Checked ? idxChecked : idxUnchecked);

                        if(syncParentChildCheckedState && node.StateImageIndex == (int)NodeCheckState.Mixed)
                            extArgs.StateImageIndex = idxMixed;
                    }
                }

                if(extArgs.StateImageIndex != -1)
                    extArgs.NodeParts |= NodeParts.StateImage;
                #endregion

                #region Determine the colors
                // Determine the colors to use
                lineColor = base.LineColor;

                if((e.State & (TreeNodeStates.Focused | TreeNodeStates.Selected)) == (TreeNodeStates.Focused |
                  TreeNodeStates.Selected))
                {
                    if(base.FullRowSelect)
                    {
                        bgColor = SystemColors.Highlight;
                        lineColor = SystemColors.HighlightText;
                    }
                    else
                    {
                        bgColor = node.BackColor.IsEmpty ? base.BackColor : node.BackColor;
                        bgTextColor = SystemColors.Highlight;
                    }

                    fgColor = SystemColors.HighlightText;
                }
                else
                {
                    if((e.State & TreeNodeStates.Selected) == 0 || base.HideSelection || !base.Enabled)
                    {
                        bgColor = node.BackColor.IsEmpty ? base.BackColor : node.BackColor;
                        fgColor = node.ForeColor.IsEmpty ? base.ForeColor : node.ForeColor;
                    }
                    else
                    {
                        if(base.FullRowSelect)
                        {
                            bgColor = SystemColors.Control;
                            lineColor = SystemColors.ControlText;
                        }
                        else
                        {
                            bgColor = node.BackColor.IsEmpty ? base.BackColor : node.BackColor;
                            bgTextColor = SystemColors.Control;
                        }

                        fgColor = SystemColors.ControlText;
                    }
                }

                extArgs.BackgroundBrush = new SolidBrush(bgColor);
                extArgs.TextForegroundBrush = new SolidBrush(fgColor);

                if(!bgTextColor.IsEmpty)
                    extArgs.TextBackgroundBrush = new SolidBrush(bgTextColor);

                extArgs.LinePen = new Pen(lineColor, 1) { DashStyle = DashStyle.Dot };

                #endregion

                #region Determine the line positions

                // Position the line based on the image height.  The line position also determines the expando
                // position.
                height = (imgHeight + 2) / 2;
                extArgs.LinePosition = bounds.Left + (height / 2) + 7 +
                    (base.Indent * (level - (base.ShowRootLines ? 0 : 1)));
                extArgs.LineWidth = indent - (height / 2) - 5;

                if(base.ShowLines)
                {
                    extArgs.NodeParts |= NodeParts.Lines;
                    bounds.Offset((level * indent) + 2, 0);

                    if(base.ShowRootLines)
                        bounds.Offset(indent, 0);
                }
                else
                {
                    bounds.Offset((level * indent) + 2, 0);

                    if(base.ShowPlusMinus && base.ShowRootLines)
                        bounds.Offset(indent, 0);
                }
                #endregion

                #region Determine the bounds of the expando image

                if(this.ShowPlusMinus && node.Nodes.Count != 0 && (node.Parent != null || (node.Parent == null &&
                  this.ShowRootLines)))
                {
                    extArgs.NodeParts |= NodeParts.Expando;

                    if(ilExpando != null && ilExpando.Images.Count != 0)
                        ilPM = ilExpando;
                    else
                        ilPM = ilPlusMinus;

                    // The position is based on the line position
                    height = Math.Min(ilPM.ImageSize.Height, base.ItemHeight);
                    left = extArgs.LinePosition - (height / 2);

                    if(bounds.Height % 2 != 0)
                        top = bounds.Top + ((bounds.Height - height) / 2);
                    else
                        top = bounds.Top + ((bounds.Height - height) / 2) + 1;

                    extArgs.ExpandoBounds = new Rectangle(left, top, height, height);
                }
                #endregion

                #region Determine the bounds of the checkbox or state image

                if(extArgs.StateImageIndex != -1)
                {
                    extArgs.NodeParts |= NodeParts.StateImage;

                    // Position the image based on the image height
                    height = Math.Min(ilCheckState.ImageSize.Height, bounds.Height);
                    left = bounds.Left + ((height + 2) / 4) - 1;

                    if(bounds.Height % 2 != 0)
                        top = bounds.Top + ((bounds.Height - height) / 2);
                    else
                        top = bounds.Top + ((bounds.Height - height) / 2) + 1;

                    extArgs.StateBounds = new Rectangle(left, top, height, height);
                    bounds.Offset(height + 3, 0);
                }
                #endregion

                #region Determine the bounds of the image

                if(extArgs.ImageIndex != -1)
                {
                    extArgs.NodeParts |= NodeParts.NodeImage;

                    // Position the image based on the image height
                    height = Math.Min(imgHeight, bounds.Height);
                    width = Math.Min(base.ImageList.ImageSize.Width, height);
                    top = bounds.Top + ((bounds.Height - height) / 2);

                    extArgs.ImageBounds = new Rectangle(bounds.Left + 1, top, width, height);
                    bounds.Offset(width + 3, 0);
                }
                #endregion

                #region Determine the font and the bounds of the node's text

                if(node.NodeFont == null)
                    extArgs.Font = base.Font;
                else
                    extArgs.Font = node.NodeFont;

                if((e.State & TreeNodeStates.Hot) != 0)
                    extArgs.Font = new Font(extArgs.Font, extArgs.Font.Style | FontStyle.Underline);

                extArgs.StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces |
                    StringFormatFlags.NoWrap)
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.None
                };

                // Setting the text automatically calculates text and focus bounds
                extArgs.TextBounds = new Rectangle(bounds.Left + 1, bounds.Top, 0, 0);

                if(!String.IsNullOrEmpty(node.Text))
                {
                    extArgs.NodeParts |= NodeParts.Text;
                    extArgs.Text = node.Text;
                }
                #endregion

                #region Draw parts

                // Give the user a chance to modify the parameters or draw parts before we do
                this.OnTreeNodeDrawing(extArgs);

                // Now draw the parts that they didn't draw
                if((extArgs.NodeParts & NodeParts.Background) != 0)
                    g.FillRectangle(extArgs.BackgroundBrush, e.Bounds);

                if((extArgs.NodeParts & NodeParts.Lines) != 0)
                {
                    lineLevel = level;
                    parent = node;
                    left = extArgs.LinePosition;
                    width = extArgs.LineWidth;
                    bounds = e.Bounds;

                    // Draw lines from the node out to the highest root node
                    while(lineLevel != 0 && parent != null)
                    {
                        if(parent.NextNode != null || lineLevel == level)
                            ExtendedTreeView.DrawLineParts(g, extArgs.LinePen, parent, bounds, left, width,
                                (lineLevel != level));

                        lineLevel--;
                        left -= indent;
                        parent = parent.Parent;
                    }

                    if(base.ShowRootLines)
                    {
                        if(node.Parent == null)
                            ExtendedTreeView.DrawLineParts(g, extArgs.LinePen, node, bounds, left, width, false);
                        else
                        {
                            if(parent != null && parent.NextNode != null)
                                ExtendedTreeView.DrawLineParts(g, extArgs.LinePen, node, bounds, left, width, true);
                        }
                    }
                }

                if((extArgs.NodeParts & NodeParts.Expando) != 0)
                {
                    if(node.IsExpanded)
                    {
                        ilPM.Draw(g, extArgs.ExpandoBounds.Left, extArgs.ExpandoBounds.Top,
                            extArgs.ExpandoBounds.Width, extArgs.ExpandoBounds.Height, idxCollapse);
                    }
                    else
                    {
                        ilPM.Draw(g, extArgs.ExpandoBounds.Left, extArgs.ExpandoBounds.Top,
                            extArgs.ExpandoBounds.Width, extArgs.ExpandoBounds.Height, idxExpand);
                    }
                }

                if((extArgs.NodeParts & NodeParts.StateImage) != 0)
                {
                    ilCheckState.Draw(g, extArgs.StateBounds.Left, extArgs.StateBounds.Top,
                        extArgs.StateBounds.Width, extArgs.StateBounds.Height, extArgs.StateImageIndex);
                }

                if((extArgs.NodeParts & NodeParts.NodeImage) != 0)
                {
                    base.ImageList.Draw(g, extArgs.ImageBounds.Left, extArgs.ImageBounds.Top,
                        extArgs.ImageBounds.Width, extArgs.ImageBounds.Height, extArgs.ImageIndex);
                }

                if((extArgs.NodeParts & NodeParts.Text) != 0)
                {
                    if(extArgs.TextBackgroundBrush != null)
                        g.FillRectangle(extArgs.TextBackgroundBrush, extArgs.TextBounds);

                    if(base.Enabled)
                    {
                        g.DrawString(extArgs.Text, extArgs.Font, extArgs.TextForegroundBrush, extArgs.TextBounds,
                            extArgs.StringFormat);
                    }
                    else
                    {
                        ControlPaint.DrawStringDisabled(g, extArgs.Text, extArgs.Font, bgColor, extArgs.TextBounds,
                            extArgs.StringFormat);
                    }

                    // Draw the focus rectangle on the focused item
                    if((e.State & TreeNodeStates.Focused) != 0)
                    {
                        ControlPaint.DrawFocusRectangle(g, (!base.FullRowSelect) ? extArgs.TextBounds : e.Bounds,
                            fgColor, (!bgTextColor.IsEmpty) ? bgTextColor : bgColor);
                    }
                }

                // Give the user a chance to modify the display after we've finished drawing it
                this.OnTreeNodeDrawn(extArgs);
                #endregion
            }
            finally
            {
                // This gets rid of any graphics objects that we created or that the user created and assigned to
                // the arguments.
                extArgs?.Dispose();
            }
        }

        /// <summary>
        /// This is overridden to synchronize the parent and child checked states if
        /// <see cref="SyncParentChildCheckedState"/> is set to true.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if(e != null && syncParentChildCheckedState)
            {
                TreeNode node = e.Node;
                bool hasOtherCheckedItems = false;

                // If changing the check state of a parent node, change the check state of higher parent nodes
                // but don't do anything else.
                if(checkingParent)
                {
                    if(node.Checked && node.Parent != null && node.Parent.Checked == false)
                        node.Parent.Checked = true;

                    base.OnAfterCheck(e);
                    return;
                }

                try
                {
                    this.BeginUpdate();

                    if(node.Checked && node.Parent != null && node.Parent.Checked == false)
                    {
                        try
                        {
                            checkingParent = true;
                            node.Parent.Checked = true;
                        }
                        finally
                        {
                            checkingParent = false;
                        }
                    }

                    if(node.Nodes.Count != 0)
                        foreach(TreeNode subNode in node.Nodes)
                            if(subNode.Checked != node.Checked)
                                subNode.Checked = node.Checked;

                    // If unchecked and it was the last checked item for the parent, uncheck the parent too
                    if(!node.Checked && node.Parent != null)
                    {
                        foreach(TreeNode subNode in node.Parent.Nodes)
                            if(subNode.Checked)
                            {
                                hasOtherCheckedItems = true;
                                break;
                            }

                        if(!hasOtherCheckedItems)
                            node.Parent.Checked = false;
                    }

                    node.StateImageIndex = (int)(node.Checked ? NodeCheckState.Checked : NodeCheckState.Unchecked);

                    UpdateParentImageState(node);
                }
                finally
                {
                    this.EndUpdate();
                }
            }

            base.OnAfterCheck(e);
        }

        /// <summary>
        /// This is overridden to suppress the collapse if both <see cref="TreeView.ShowPlusMinus"/> and
        /// <see cref="AllowCollapse"/> are false.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            if(e != null && !this.ShowPlusMinus && !allowCollapse)
                e.Cancel = true;
            else
                base.OnBeforeCollapse(e);
        }

        /// <summary>
        /// This is overridden to implement additional behavior on mouse up events
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>When <see cref="TreeView.FullRowSelect"/> is enabled along with <see cref="TreeView.ShowLines"/>
        /// this ensures that the node becomes selected.  When a <see cref="TreeView.StateImageList"/> is defined
        /// and a node's state image is clicked the <see cref="ChangeStateImage"/> event is raised.</remarks>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            TreeViewHitTestInfo hit = this.HitTest(e.Location);

            if(this.FullRowSelect && this.ShowLines)
            {
                if((hit.Location == TreeViewHitTestLocations.RightOfLabel ||
                  hit.Location == TreeViewHitTestLocations.Indent) && hit.Node != null &&
                  hit.Node != this.SelectedNode)
                {
                    this.SelectedNode = hit.Node;
                }
            }
            else
            {
                if(!base.CheckBoxes && base.StateImageList != null &&
                  hit.Location == TreeViewHitTestLocations.StateImage && hit.Node != null &&
                  hit.Node.StateImageIndex != -1)
                {
                    OnChangeStateImage(new TreeViewEventArgs(hit.Node, TreeViewAction.ByMouse));
                }
                else
                {
                    if(hit.Location == TreeViewHitTestLocations.RightOfLabel && selectOnROLClick &&
                      hit.Node != this.SelectedNode)
                    {
                        this.SelectedNode = hit.Node;
                    }
                }
            }

            base.OnMouseUp(e);
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This can be used to manually synchronize the checked states of all parent and child nodes after
        /// adding or removing nodes from the tree.
        /// </summary>
        public void SynchronizeCheckedStates()
        {
            if(this.DesignMode || !syncParentChildCheckedState)
                return;

            this.BeginUpdate();

            foreach(TreeNode n in this)
            {
                try
                {
                    checkingParent = true;

                    if(!n.Checked && n.Nodes.Count != 0 && AnyChildChecked(n))
                        n.Checked = true;
                    else
                    {
                        if(n.Checked)
                        {
                            if(n.Nodes.Count != 0 && AllChildrenUnchecked(n))
                                n.Checked = false;

                            if(n.Parent != null && !n.Parent.Checked)
                                n.Parent.Checked = true;
                        }
                    }
                }
                finally
                {
                    checkingParent = false;
                }

                if(n.Checked)
                {
                    if(n.StateImageIndex != (int)NodeCheckState.Checked)
                        n.StateImageIndex = (int)NodeCheckState.Checked;

                    if(n.Parent != null && n.Parent.LastNode == n)
                        UpdateParentImageState(n);
                }
                else
                {
                    if(n.StateImageIndex != (int)NodeCheckState.Unchecked)
                        n.StateImageIndex = (int)NodeCheckState.Unchecked;

                    if(n.Parent != null && n.Parent.LastNode == n)
                        UpdateParentImageState(n);
                }
            }

            this.EndUpdate();
        }

        /// <summary>
        /// This can be used to see if all children of a specified tree node are checked
        /// </summary>
        /// <param name="parent">The parent node</param>
        /// <returns>True if all children are checked, false if not</returns>
        public static bool AllChildrenChecked(TreeNode parent)
        {
            if(parent == null)
                return false;

            foreach(TreeNode node in parent.Nodes)
            {
                if(!node.Checked)
                    return false;

                if(node.Nodes.Count != 0 && !AllChildrenChecked(node))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// This can be used to see if any children of a specified tree node are checked
        /// </summary>
        /// <param name="parent">The parent node</param>
        /// <returns>True if any child is checked, false if not</returns>
        public static bool AnyChildChecked(TreeNode parent)
        {
            if(parent != null)
                foreach(TreeNode node in parent.Nodes)
                {
                    if(node.Checked)
                        return true;

                    if(node.Nodes.Count != 0 && AnyChildChecked(node))
                        return true;
                }

            return false;
        }

        /// <summary>
        /// This can be used to see if all children of a specified tree node are unchecked
        /// </summary>
        /// <param name="parent">The parent node</param>
        /// <returns>True if all children are unchecked, false if not</returns>
        public static bool AllChildrenUnchecked(TreeNode parent)
        {
            if(parent == null)
                return false;

            foreach(TreeNode node in parent.Nodes)
            {
                if(node.Checked)
                    return false;

                if(node.Nodes.Count != 0 && !AllChildrenUnchecked(node))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a value indicating whether the node with the specified name is checked
        /// </summary>
        /// <param name="name">The node name to find in the tree view.</param>
        /// <returns>True if the named node is checked, false if it is not.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the node name is not found in the
        /// tree view.</exception>
        public bool GetNodeChecked(string name)
        {
            TreeNode node = this[name];

            if(node == null)
                throw new ArgumentOutOfRangeException(nameof(name), name, LR.GetString("ExNodeNameOutOfRange"));

            return node.Checked;
        }

        /// <summary>
        /// Returns a value indicating the current check state of the node with the specified name
        /// </summary>
        /// <param name="name">The node name to find in the tree view.</param>
        /// <returns>The current check state of the specified node.  A return value of <c>Mixed</c> is equivalent
        /// to <c>Checked</c>.  It indicates that the children of the node contain a mix of checked and unchecked
        /// items.  This only occurs if the <see cref="SyncParentChildCheckedState"/> property is true.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the node name is not found in the
        /// tree view.</exception>
        public NodeCheckState GetNodeCheckState(string name)
        {
            TreeNode node = this[name];

            if(node == null)
                throw new ArgumentOutOfRangeException(nameof(name), name, LR.GetString("ExNodeNameOutOfRange"));

            if(syncParentChildCheckedState)
                return (NodeCheckState)node.StateImageIndex;
            else
                return (node.Checked) ? NodeCheckState.Checked : NodeCheckState.Unchecked;
        }

        /// <summary>
        /// Sets the check state of the node with the specified name to <c>Checked</c> or <c>Unchecked</c>
        /// </summary>
        /// <param name="name">The node name to find in the tree view</param>
        /// <param name="check">True to check the node, false to uncheck it</param>
        /// <returns>The node with the specified name or null if the node could not be found</returns>
        public TreeNode SetNodeChecked(string name, bool check)
        {
            TreeNode node = this[name];

            if(node != null)
                node.Checked = check;

            return node;
        }

        /// <summary>
        /// Clear all currently checked nodes by setting them to an unchecked state
        /// </summary>
        public void ClearSelections()
        {
            this.BeginUpdate();

            foreach(TreeNode n in this)
                if(n.Checked)
                    n.Checked = false;

            this.EndUpdate();
        }

        /// <summary>
        /// Select all nodes by setting their state to checked.
        /// </summary>
        public void SelectAll()
        {
            this.BeginUpdate();

            foreach(TreeNode n in this)
                if(!n.Checked)
                    n.Checked = true;

            this.EndUpdate();
        }
        #endregion
    }
}
