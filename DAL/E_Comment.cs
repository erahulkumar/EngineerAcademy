//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class E_Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string NameSurName { get; set; }
        public string Email { get; set; }
        public string CommentContent { get; set; }
        public bool isApproved { get; set; }
        public Nullable<int> ApproveUserID { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public System.DateTime addDate { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> LastUpdateUserID { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
    
        public virtual E_Post E_Post { get; set; }
        public virtual E_User E_User { get; set; }
    }
}