﻿using HumanResources.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.Models
{
    public class Permit
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "İzin Tipi")]
        public PermitType Type { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}