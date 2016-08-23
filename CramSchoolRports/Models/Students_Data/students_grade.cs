namespace CramSchoolReports.Models.Students_Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class students_grade
    {
        [Key]
        [Display(Name = "成績管理番号")]
        public long students_grade_id { get; set; }

        [Display(Name = "生徒管理番号")]
        public string students_id { get; set; }

        [Display(Name = "試験日")]
        [DataType(DataType.Date)]
        public DateTime exam_date { get; set; }

        [Display(Name = "試験区分")]
        [Required]
        public long exam_id { get; set; }

        [Display(Name = "教科番号")]
        public long class_id { get; set; }

        [Display(Name = "試験点数")]
        public long exam_scores { get; set; }

        [Display(Name = "席次")]
        public long exam_precedence { get; set; }

        public string create_user { get; set; }

        public string create_date { get; set; }

        public string update_user { get; set; }

        public string update_date { get; set; }

        public virtual Models.Students_M.students_m students_m { get; set; }

        public virtual Models.Settings_M.exams_m exams_m { get; set; }

        public virtual Models.Settings_M.classes_m classes_m { get; set; }

        public long average_score
        {
            get
            {
                return get_average();
            }
        }

        public long get_average()
        {
            Models.Settings_M.MastersModel MasterDB = new Models.Settings_M.MastersModel();
            var average_score = MasterDB.average_scores_m.SingleOrDefault(
                                    x => x.exam_date == exam_date && 
                                         x.exam_id == exam_id && 
                                         x.class_id == class_id && 
                                         x.school_id == students_m.school_id);
            if (average_score != null)
            {
                return average_score.exam_scores;
            }
            return 0;

        }
    }
}
