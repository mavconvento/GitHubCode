using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceFactory
{
    public interface IMember
    {
        Boolean Save();
        Boolean MemberRingSave();
        Boolean ReaderRegistrationSave();
        Boolean RFIDRegisterSave();
        void MemberDetailsSelectAll(DataGridView memberList);
        DataTable ExportEClockMasterlist();
        DataSet MemberDetailsSearchByKey();
        DataTable MemberRingSearchByKey();
        Boolean MemberDetailsDelete();
        Boolean MemberRingDelete();
        DataSet GetMemberDistance();
        DataTable GetMembetDistancePerReleasePoint();

    }
}
