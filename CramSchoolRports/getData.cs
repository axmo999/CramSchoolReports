﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CramSchoolReports.Models;
using System.Diagnostics;
using System.Data;
using System.Data.Entity;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.IO;

namespace CramSchoolReports
{
    class getData
    {
        /// <summary>生徒マスタ情報コンテキスト</summary>
        private Models.Students_M.Students_mModel student_m_db = new Models.Students_M.Students_mModel();

        /// <summary>生徒登録情報コンテキスト</summary>
        private Models.Students_Data.StudentsModel studentdb = new Models.Students_Data.StudentsModel();

        /// <summary>設定情報コンテキスト</summary>
        private Models.Settings_M.MastersModel setdb = new Models.Settings_M.MastersModel();

        /// <summary>環境依存対応Today</summary>
        private DateTime _today = Commons.Utility.Today();

        /// <summary>
        /// 年取得
        /// </summary>
        /// <returns></returns>
        public string[] getYearData()
        {
            int[] years_int = studentdb
                                .students_attendance
                                .GroupBy(x => x.attendance_day.Year)
                                .Select(x => x.Key)
                                .OrderByDescending(x => x)
                                .ToArray();

            string[] years_string = Array.ConvertAll(years_int, delegate(int value)
            {
                return value.ToString();
            });

            return years_string;
        }

        /// <summary>
        /// 月取得
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string[] getMonthData(int year)
        {
            int[] months_int = studentdb
                                .students_attendance
                                .Where(x => x.attendance_day.Year == year)
                                .GroupBy(x => x.attendance_day.Month)
                                .Select(x => x.Key)
                                .OrderByDescending(x => x)
                                .ToArray();

            string[] month_string = Array.ConvertAll(months_int, delegate(int value)
            {
                return value.ToString();
            });

            return month_string;
        }

        /// <summary>
        /// 教室取得
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, string> getOffice()
        {
            return setdb
                    .offices_m
                    .Select(x => new { id = x.office_id, name = x.name })
                    .ToDictionary(x => x.id, x => x.name);
        }

        /// <summary>
        /// 出席リスト作成
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="OfficeNum"></param>
        /// <returns></returns>
        public Array getAttend(int Year, int Month, int OfficeNum)
        {

            try
            {
                // 変数より月の最初と最終日を設定
                DateTime FDM = Commons.Utility.getFDM(Year, Month);
                DateTime LDM = Commons.Utility.getLDM(Year, Month);

                // 当月の設定出席回数を取得
                var attend_count = setdb.atteds_m.SingleOrDefault(x => x.year_month == FDM);

                decimal week1 = attend_count.g0_count == 0 ? 1 : attend_count.g0_count;

                decimal element = attend_count.g1_count == 0 ? 1 : attend_count.g1_count;

                decimal j12 = attend_count.g2_count == 0 ? 1 : attend_count.g2_count;

                decimal j3 = attend_count.g3_count == 0 ? 1 : attend_count.g3_count;

                // 当月の出席リストを取得
                var student_attend_list = studentdb
                                    .students_attendance
                                    .Where(x => x.attendance_day >= FDM && x.attendance_day <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .GroupBy(x => x.students_id)
                                    .Select(x => new
                                    {
                                        name = x.FirstOrDefault().students_m,
                                        count = x.Count(),
                                        absence = x.Where(s => s.start_time == s.end_time).Count()
                                    })
                                    .ToList()
                                    .Select(x => new 
                                        {
                                            name = x.name.display_name,
                                            count = x.count - x.absence,
                                            per = (Convert.ToDecimal(x.count - x.absence) / getNeccesaryNum(x.name.grade, week1, element, j12, j3) * 100).ToString("0.0") + "%",
                                            neccesarycount = getNeccesaryNum(x.name.grade, week1, element, j12, j3),
                                            school = x.name.schools_m.name,
                                            grade = x.name.grade,
                                            division = Convert.ToInt32(x.name.schools_m.division_id)
                                        })
                                    .ToArray();

                return student_attend_list;

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }


        /// <summary>
        /// 指導リスト作成
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="OfficeNum"></param>
        /// <returns></returns>
        public Array getGuide(int Year, int Month, int OfficeNum)
        {

            try
            {
                // 変数より月の最初と最終日を設定
                DateTime FDM = Commons.Utility.getFDM(Year, Month);
                DateTime LDM = Commons.Utility.getLDM(Year, Month);


                // 当月の指導リストを取得
                var student_guid_list = studentdb
                                    .students_guide
                                    .Where(x => x.guide_date >= FDM && x.guide_date <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .Select(x => new
                                    {
                                        students_m = x.students_m,
                                        guide_date = x.guide_date,
                                        classes_m = x.classes_m,
                                        guide_contents = x.guide_contents,
                                        teachers_m = x.teachers_m
                                    })
                                    .ToList()
                                    .Select(x => new
                                    {
                                        name = x.students_m.display_name,
                                        school = x.students_m.schools_m.name,
                                        grade = x.students_m.grade,
                                        division = x.students_m.schools_m.division_id,
                                        date = x.guide_date.ToString("yyyy-MM-dd"),
                                        classes = x.classes_m.display_name,
                                        guid = x.guide_contents,
                                        teacher = x.teachers_m.display_name
                                    })
                                    .ToArray();

                return student_guid_list;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// 自立リスト作成
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="OfficeNum"></param>
        /// <returns></returns>
        public Array getIndependent(int Year, int Month, int OfficeNum)
        {

            try
            {
                // 変数より月の最初と最終日を設定
                DateTime FDM = Commons.Utility.getFDM(Year, Month);
                DateTime LDM = Commons.Utility.getLDM(Year, Month);


                // 当月の自立リストを取得
                var student_independent_list = studentdb
                                    .students_independence
                                    .Where(x => x.week >= FDM && x.week <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .Select(x => new
                                    {
                                        students_m = x.students_m,
                                        guide_date = x.week,
                                        teachers_m = x.teachers_m,
                                        q1 = x.question01,
                                        q2 = x.question02,
                                        q3 = x.question03,
                                        q4 = x.question04,
                                        q5 = x.question05,
                                        q6 = x.question06,
                                        q7 = x.question07,
                                        q8 = x.question08,
                                        q9 = x.question09,
                                        q10 = x.question10,
                                        q11 = x.question11,
                                        q12 = x.question12,
                                        q13 = x.question13,
                                        q14 = x.question14,
                                        q15 = x.question15,
                                    })
                                    .ToList()
                                    .Select(x => new
                                    {
                                        name = x.students_m.display_name,
                                        school = x.students_m.schools_m.name,
                                        grade = x.students_m.grade,
                                        division = x.students_m.schools_m.division_id,
                                        date = x.guide_date.ToString("yyyy-MM-dd"),
                                        teacher = x.teachers_m.display_name,
                                        q1 = x.q1,
                                        q2 = x.q2,
                                        q3 = x.q3,
                                        q4 = x.q4,
                                        q5 = x.q5,
                                        q6 = x.q6,
                                        q7 = x.q7,
                                        q8 = x.q8,
                                        q9 = x.q9,
                                        q10 = x.q10,
                                        q11 = x.q11,
                                        q12 = x.q12,
                                        q13 = x.q13,
                                        q14 = x.q14,
                                        q15 = x.q15,
                                        avr = (x.q1 
                                            + x.q2
                                            + x.q3
                                            + x.q4
                                            + x.q5
                                            + x.q6
                                            + x.q7
                                            + x.q8
                                            + x.q9
                                            + x.q10
                                            + x.q11
                                            + x.q12
                                            + x.q13
                                            + x.q14
                                            + x.q15)
                                            / 15,
                                        rank1 = (x.q1 + x.q2 + x.q3 + x.q4 + x.q5) / 5,
                                        rank2 = (x.q6 + x.q7 + x.q8 + x.q9) / 4,
                                        rank3 = (x.q10 + x.q11 + x.q12 + x.q13) / 4,
                                        rank4 = (x.q14 + x.q15) / 2
                                    })
                                    .ToArray();

                return student_independent_list;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }


        public Array getReport(int Year, int Month, int OfficeNum)
        {

            try
            {
                // 変数より月の最初と最終日を設定
                DateTime FDM = Commons.Utility.getFDM(Year, Month);
                DateTime LDM = Commons.Utility.getLDM(Year, Month);

                // 当月の設定出席回数を取得
                var attend_count = setdb.atteds_m.SingleOrDefault(x => x.year_month == FDM);

                decimal week1 = attend_count.g0_count == 0 ? 1 : attend_count.g0_count;

                decimal element = attend_count.g1_count == 0 ? 1 : attend_count.g1_count;

                decimal j12 = attend_count.g2_count == 0 ? 1 : attend_count.g2_count;

                decimal j3 = attend_count.g3_count == 0 ? 1 : attend_count.g3_count;

                // 当月の学習生活リストを取得
                var student_reports = studentdb
                                        .monthly_reports
                                        .Where(x => x.monthly_report_date >= FDM && x.monthly_report_date <= LDM)
                                        .Include(s => s.students_m).Include(s => s.teachers_m)
                                        .Where(x => x.students_m.office_id == OfficeNum).ToList()
                                        .Select(x => new Models.Reports.ReportsModel() {
                                            name = x.students_m.display_name,
                                            school = x.students_m.schools_m.name,
                                            grade = x.students_m.grade,
                                            division = Convert.ToInt32(x.students_m.schools_m.division_id),
                                            study = x.study_contents,
                                            life = x.life_contents
                                        }).ToList();


                // 当月の出席リストを取得
                var student_attend_list = studentdb
                                    .students_attendance
                                    .Where(x => x.attendance_day >= FDM && x.attendance_day <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .GroupBy(x => x.students_id)
                                    .Select(x => new { name = x.FirstOrDefault().students_m,
                                                       count = x.Count(),
                                                       absence = x.Where(s => s.start_time == s.end_time).Count()
                                                     })
                                    .ToList()
                                    .Select(x => new Models.Reports.ReportsModel() { 
                                        name = x.name.display_name, 
                                        school = x.name.schools_m.name, 
                                        grade = x.name.grade, 
                                        division = Convert.ToInt32(x.name.schools_m.division_id),
                                        count = x.count - x.absence,
                                        per = (Convert.ToDecimal(x.count - x.absence) / getNeccesaryNum(x.name.grade, week1, element, j12, j3) * 100).ToString("0.0") + "%",
                                        neccesarycount = (int)getNeccesaryNum(x.name.grade, week1, element, j12, j3),
                                        absence = x.absence
                                    })
                                    .ToList();

                // 当月の自立リストを取得
                var student_independent_list = studentdb
                                    .students_independence
                                    .Where(x => x.week >= FDM && x.week <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .Select(x => new
                                    {
                                        students_m = x.students_m,
                                        guide_date = x.week,
                                        teachers_m = x.teachers_m,
                                        q1 = x.question01,
                                        q2 = x.question02,
                                        q3 = x.question03,
                                        q4 = x.question04,
                                        q5 = x.question05,
                                        q6 = x.question06,
                                        q7 = x.question07,
                                        q8 = x.question08,
                                        q9 = x.question09,
                                        q10 = x.question10,
                                        q11 = x.question11,
                                        q12 = x.question12,
                                        q13 = x.question13,
                                        q14 = x.question14,
                                        q15 = x.question15,
                                    })
                                    .ToList()
                                    .Select(x => new
                                    {
                                        name = x.students_m.display_name,
                                        school = x.students_m.schools_m.name,
                                        grade = x.students_m.grade,
                                        division = Convert.ToInt32(x.students_m.schools_m.division_id),
                                        date = x.guide_date.ToString("yyyy-MM-dd"),
                                        teacher = x.teachers_m.display_name,
                                        q1 = x.q1,
                                        q2 = x.q2,
                                        q3 = x.q3,
                                        q4 = x.q4,
                                        q5 = x.q5,
                                        q6 = x.q6,
                                        q7 = x.q7,
                                        q8 = x.q8,
                                        q9 = x.q9,
                                        q10 = x.q10,
                                        q11 = x.q11,
                                        q12 = x.q12,
                                        q13 = x.q13,
                                        q14 = x.q14,
                                        q15 = x.q15,
                                        avr = (x.q1
                                            + x.q2
                                            + x.q3
                                            + x.q4
                                            + x.q5
                                            + x.q6
                                            + x.q7
                                            + x.q8
                                            + x.q9
                                            + x.q10
                                            + x.q11
                                            + x.q12
                                            + x.q13
                                            + x.q14
                                            + x.q15)
                                            / 15,
                                        rank1 = (x.q1 + x.q2 + x.q3 + x.q4 + x.q5) / 5,
                                        rank2 = (x.q6 + x.q7 + x.q8 + x.q9) / 4,
                                        rank3 = (x.q10 + x.q11 + x.q12 + x.q13) / 4,
                                        rank4 = (x.q14 + x.q15) / 2
                                    })
                                    .GroupBy(x => new
                                    {
                                        x.name,
                                        x.school,
                                        x.grade,
                                        x.division
                                    })
                                    .Select(x => new Models.Reports.ReportsModel() 
                                    {
                                        name = x.Key.name,
                                        school = x.Key.school,
                                        grade = x.Key.grade,
                                        division = x.Key.division,
                                        q1 = x.Average(a => a.q1).ToString("0.0"),
                                        q2 = x.Average(a => a.q2).ToString("0.0"),
                                        q3 = x.Average(a => a.q3).ToString("0.0"),
                                        q4 = x.Average(a => a.q4).ToString("0.0"),
                                        q5 = x.Average(a => a.q5).ToString("0.0"),
                                        q6 = x.Average(a => a.q6).ToString("0.0"),
                                        q7 = x.Average(a => a.q7).ToString("0.0"),
                                        q8 = x.Average(a => a.q8).ToString("0.0"),
                                        q9 = x.Average(a => a.q9).ToString("0.0"),
                                        q10 = x.Average(a => a.q10).ToString("0.0"),
                                        q11 = x.Average(a => a.q11).ToString("0.0"),
                                        q12 = x.Average(a => a.q12).ToString("0.0"),
                                        q13 = x.Average(a => a.q13).ToString("0.0"),
                                        q14 = x.Average(a => a.q14).ToString("0.0"),
                                        q15 = x.Average(a => a.q15).ToString("0.0"),
                                        avr = x.Average(a => a.avr).ToString("0.0")
                                    })
                                    .ToList();

                var report = student_attend_list
                            .Union(student_independent_list)
                            .Union(student_reports)
                            .GroupBy(x => new { x.name, x.school, x.grade, x.division })
                            .Select(x => new Models.Reports.ReportsModel()
                            {
                                name = x.Key.name,
                                school = x.Key.school,
                                grade = x.Key.grade,
                                division = x.Key.division,
                                count = x.Max(a => a.count),
                                per = x.Max(a => a.per),
                                neccesarycount = x.Max(a => a.neccesarycount),
                                absence = x.Max(a => a.absence),
                                q1 = x.Max(a => a.q1),
                                q2 = x.Max(a => a.q2),
                                q3 = x.Max(a => a.q3),
                                q4 = x.Max(a => a.q4),
                                q5 = x.Max(a => a.q5),
                                q6 = x.Max(a => a.q6),
                                q7 = x.Max(a => a.q7),
                                q8 = x.Max(a => a.q8),
                                q9 = x.Max(a => a.q9),
                                q10 = x.Max(a => a.q10),
                                q11 = x.Max(a => a.q11),
                                q12 = x.Max(a => a.q12),
                                q13 = x.Max(a => a.q13),
                                q14 = x.Max(a => a.q14),
                                q15 = x.Max(a => a.q15),
                                avr = x.Max(a => a.avr),
                                study = x.Max(a => a.study),
                                life = x.Max(a => a.life)
                            });

                return report.ToArray();

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }

        public decimal getNeccesaryNum(string grade, decimal week1, decimal element, decimal j12, decimal j3)
        {
            switch (grade)
            {
                case "中学校１年生":
                    return j12;
                    
                case "中学校２年生":
                    return j12;

                case "中学校３年生":
                    return j3;

                case "週１":
                    return week1;

                default:
                    return element;

            } 
        }

    }
}
