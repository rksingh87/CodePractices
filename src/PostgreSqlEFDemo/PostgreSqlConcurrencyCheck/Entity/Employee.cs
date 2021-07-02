using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostgreSqlConcurrencyCheck.Entity
{

    [Table("employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        
        [Column("name")]
        public string name { get; set; }

        [Column("count")]
        public int count { get; set; }

        [Column("xmin")]
        public uint xmin { get; set; }
    }
}
