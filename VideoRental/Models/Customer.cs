using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoRental.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public MemberShipType MemberShipType { get; set; }

        [Display(Name ="Membership Type")]
        public byte MemberShipTypeId { get; set; }

        [Display(Name ="Date Of Birth")]
        [IfMember18YearsOld]
        public DateTime? DateOfBirth { get; set; }
        public bool IsSubscribeToNewsLetter { get; set; }
    }
}