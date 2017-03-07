using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingBook.Models.ViewModel
{
    public class AccountingBookViewModel
    {
        [DisplayName("類別")]
        public string Category { get; set; }
        [DisplayName("日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }
        [DisplayName("金額")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Money { get; set; }
    }
}