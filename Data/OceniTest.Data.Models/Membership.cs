namespace OceniTest.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OceniTest.Data.Common.Models;

    public class Membership : BaseDeletableModel<string>
    {
        public Membership()
        {
            this.Id = Guid.NewGuid().ToString();
            this.MembershipUsers = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> MembershipUsers { get; set; }
    }
}
