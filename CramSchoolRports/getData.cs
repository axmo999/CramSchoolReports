using System;
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
                Decimal attend_count = setdb.atteds_m.SingleOrDefault(x => x.year_month == FDM).count;

                // 当月の出席リストを取得
                var student_attend_list = studentdb
                                    .students_attendance
                                    .Where(x => x.attendance_day >= FDM && x.attendance_day <= LDM)
                                    .Include(s => s.students_m)
                                    .Where(x => x.students_m.office_id == OfficeNum)
                                    .GroupBy(x => x.students_id)
                                    .Select(x => new { name = x.FirstOrDefault().students_m, count = x.Count() })
                                    .ToList()
                                    .Select(x => new { name = x.name.display_name, count = x.count, per = Convert.ToDecimal(x.count) / attend_count * 100 + "%", school = x.name.schools_m.name, grade = x.name.grade, division = Convert.ToInt32(x.name.schools_m.division_id) })
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
                                        avr = x.q1 
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
                                            + x.q15
                                            / 15
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


    }
}
