//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseComboBoxDesigner.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/09/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This contains a control designer for BaseComboBox that enables the display of snap lines for it
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/22/2010  EFW  Created the code
//===============================================================================================================

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace EWSoftware.ListControls.Design
{
    /// <summary>
    /// This is a control designer for the <see cref="BaseComboBox"/> class that enables snap lines for it
    /// </summary>
    internal class BaseComboBoxDesigner : ControlDesigner
    {
        /// <summary>
        /// Overrides the base implementation of the SnapLines property and this override is what allows us to
        /// configure the line to run through the textbox in the VS designer.
        /// </summary>
        public override IList SnapLines
        {
            get
            {
                // Get/initialize the snapLines from the base implementation For reference, the base
                // implementation returns the appropriate margins for the control and the default snap lines
                // (without a  baseline).  The default values returned are listed below this override copied
                // from Reflector.
                IList snapLines = base.SnapLines;

                // Get a handle to the target control type.  If it fails, return the default
                if(Control is BaseComboBox control)
                {
                    // Create a new instance of the IDesigner based off of the target textbox in the BaseComboBox
                    // user control.
                    IDesigner designer = TypeDescriptor.CreateDesigner(control.txtValue, typeof(IDesigner));

                    if(designer != null)
                    {
                        using(designer)
                        {
                            designer.Initialize(control.txtValue);

                            if(designer is ControlDesigner boxDesigner)
                            {
                                // Go through each SnapLine in the designer, look for the BaseLine, and create a
                                // new one for the control.
                                foreach(SnapLine line in boxDesigner.SnapLines)
                                {
                                    if(line.SnapLineType == SnapLineType.Baseline)
                                    {
                                        snapLines.Add(new SnapLine(SnapLineType.Baseline,
                                            line.Offset + control.txtValue.Top, line.Filter, line.Priority));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                return snapLines;
            }
        }
    }
}
