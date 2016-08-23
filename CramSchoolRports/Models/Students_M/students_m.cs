﻿namespace CramSchoolReports.Models.Students_M
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using CramSchoolReports.Commons;

    public partial class students_m
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public students_m()
        {
            students_attendance = new HashSet<CramSchoolReports.Models.Students_Data.students_attendance>();
            students_grade = new HashSet<CramSchoolReports.Models.Students_Data.students_grade>();
            students_guide = new HashSet<CramSchoolReports.Models.Students_Data.students_guide>();
            students_like_dislike = new HashSet<CramSchoolReports.Models.Students_Data.students_like_dislike>();
            students_independence = new HashSet<CramSchoolReports.Models.Students_Data.students_independence>();
        }

        [Key]
        [Display(Name = "生徒管理番号")]
        public string students_id { get; set; }

        [Required]
        [StringLength(2147483647)]
        [Display(Name = "姓")]
        public string last_name { get; set; }

        [Required]
        [StringLength(2147483647)]
        [Display(Name = "名")]
        public string first_name { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "ミドルネーム")]
        public string middle_name { get; set; }

        [Display(Name = "学校管理番号")]
        public long school_id { get; set; }

        [Display(Name = "性別")]
        public long gender_id { get; set; }

        [Required]
        [StringLength(2147483647)]
        [Display(Name = "誕生日")]
        [DataType(DataType.Date)]
        public string birthday { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "部活")]
        public string club { get; set; }

        [Display(Name = "教室管理番号")]
        public long office_id { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "郵便番号")]
        public string postal_code { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "住所")]
        public string address { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "電話番号")]
        public string phone_number { get; set; }

        [Display(Name = "希望校")]
        public long hope_school { get; set; }

        [Display(Name = "入学校")]
        public long enter_school { get; set; }

        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "備考")]
        public string note { get; set; }

        [Display(Name = "月曜日")]
        public bool attend_mon { get; set; }

        [Display(Name = "火曜日")]
        public bool attend_tue { get; set; }

        [Display(Name = "水曜日")]
        public bool attend_wed { get; set; }

        [Display(Name = "木曜日")]
        public bool attend_thurs { get; set; }

        [Display(Name = "金曜日")]
        public bool attend_fri { get; set; }

        public string create_user { get; set; }

        public string create_date { get; set; }

        public string update_user { get; set; }

        public string update_date { get; set; }

        public virtual Models.Settings_M.gender_m gender_m { get; set; }

        public virtual Models.Settings_M.schools_m schools_m { get; set; }

        public virtual Models.Settings_M.offices_m offices_m { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Models.Students_Data.students_attendance> students_attendance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Models.Students_Data.students_grade> students_grade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Models.Students_Data.students_guide> students_guide { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Models.Students_Data.students_like_dislike> students_like_dislike { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Models.Students_Data.students_independence> students_independence { get; set; }


        [NotMapped]
        [Display(Name = "生徒名")]
        public string display_name
        {
            get
            {
                return studentName();
            }
        }

        [Display(Name = "学年")]
        public string grade
        {
            get
            {
                return Utility.GradeCal(Utility.AgeManCal(birthday));
            }
        }

        public string hope_school_name
        {
            get
            {
                return Utility.GetSchoolName(hope_school);
            }
        }

        public string enter_school_name
        {
            get
            {
                return Utility.GetSchoolName(enter_school);
            }
        }

        [Display(Name = "4/1時の満年齢 5歳以下は0")]
        public long gradeint
        {
            get
            {
                return Utility.AgeManCal(birthday);
            }
        }

        public string studentName()
        {
            string studentName = string.Empty;
            if (last_name != null)
            {
                studentName = last_name.ToString();
            }

            if (middle_name != null)
            {
                studentName += " " + middle_name.ToString();
            }

            if (first_name != null)
            {
                studentName += " " + first_name.ToString();
            }
            return studentName;
        }

        //public virtual ICollection<students_face> students_face { get; set; }

        //public virtual ICollection<CramSchoolManagement.Areas.Settings.Models.age_m> age_m { get; set; }
    }
}
