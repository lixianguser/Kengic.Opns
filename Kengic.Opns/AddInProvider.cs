using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Kengic.Opns.Database;
using Siemens.Engineering;
using Siemens.Engineering.AddIn;
using Siemens.Engineering.AddIn.Menu;

namespace Kengic.Opns
{
    public sealed class AddInProvider : ProjectTreeAddInProvider
    {
        public static TiaPortal _tiaPortal;

        public AddInProvider(TiaPortal tiaPortal)
        {
            _tiaPortal = tiaPortal;
            try
            {
                Admin.Login();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override IEnumerable<ContextMenuAddIn> GetContextMenuAddIns()
        {
            yield return new Universal.AddIn(_tiaPortal);
        }
    }
}