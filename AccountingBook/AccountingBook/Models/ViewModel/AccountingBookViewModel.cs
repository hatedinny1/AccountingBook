using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AccountingBook.Models.Enum;

namespace AccountingBook.Models.ViewModel
{
    public class AccountingBookViewModel
    {
        [DisplayName("類別")]
        public CategoryEnum Category { get; set; }

        [DisplayName("日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        [DisplayName("金額")]
        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        public decimal Money { get; set; }

        [DisplayName("備註")]
        public string Remark { get; set; }
    }
}