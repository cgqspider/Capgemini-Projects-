//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UASAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbapplicant
    {
        public tbapplicant()
        {
            this.tbadmissions = new HashSet<tbadmission>();
            this.tbeducations = new HashSet<tbeducation>();
        }
    
        public int appid { get; set; }
        public string appfname { get; set; }
        public string appfcontact { get; set; }
        public string appname { get; set; }
        public string appemail { get; set; }
        public string apppwd { get; set; }
        public string appcontact { get; set; }
        public string appaddress { get; set; }
        public string appgender { get; set; }
        public System.DateTime appDOB { get; set; }
    
        public virtual ICollection<tbadmission> tbadmissions { get; set; }
        public virtual ICollection<tbeducation> tbeducations { get; set; }
    }
}
