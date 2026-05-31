using System;
using System.Linq;
using System.Windows.Forms;

namespace Tachufind
{
    // THIS shows ZERO Reverences all over, but THIS IS NEEDED
    internal static class FormReferenceManager   
    {
        internal static bool newFormAlert = false;

        public static Form FindFormByName(string formName)
        {
            // Check if a form with the specified name exists in the open forms collection
            Form? foundForm = Application.OpenForms.OfType<Form>()
                .FirstOrDefault(form => form.Name.Equals(formName, StringComparison.OrdinalIgnoreCase));

            // If foundForm is null, return a new empty Form instead
            if (foundForm == null)
            {
                newFormAlert = true;
                return new Form();
            }
            return foundForm;
        }

        public static void InvokeIfRequired(Form form, Action action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}


/***
 *                USAGE:
 *                Form frmSaved = FormReferenceManager.FindFormByName("RTBKBShortcuts");
 *                if (frmSaved != null) 
 *                {
 *                    frmSaved.Visible = true;
 *                }
 * 
 ***/

