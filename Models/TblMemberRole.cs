using System;
using System.Collections.Generic;

namespace OnlineShoppingStore.Models;

public partial class TblMemberRole
{
    public int MemberRoleId { get; set; }

    public int? MemberId { get; set; }

    public int? RoleId { get; set; }
}
