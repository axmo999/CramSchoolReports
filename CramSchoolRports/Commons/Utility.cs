using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace CramSchoolReports.Commons
{
    public class Utility
    {

        /// <summary>
        /// クラス内コンストラクタ
        /// マスターアクセス用
        /// </summary>
        private static Models.Settings_M.MastersModel _masterdb = new Models.Settings_M.MastersModel();

        /// <summary>
        /// クラス内コンストラクタ
        /// 生徒マスターアクセス用
        /// </summary>
        private static Models.Students_Data.StudentsModel _studentdb = new Models.Students_Data.StudentsModel();

        public static DateTime Today()
        {
            TimeZoneInfo tst;

            try
            {
                tst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            }
            catch (Exception)
            {
                tst = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
            }

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), tst);
        }

        /// <summary>
        /// 全体のアプリケーションネーム
        /// </summary>
        public static string ApplicationName = "生徒管理システム";


        /// <summary>
        /// 学校名取得
        /// </summary>
        /// <param name="Id">学校管理ID</param>
        /// <returns>学校名</returns>
        public static string GetSchoolName(long Id)
        {
            var schoolname = _masterdb.schools_m.SingleOrDefault(x => x.school_id == Id);

            if (schoolname != null)
            {
                return schoolname.name;
            }

            return "なし";
        }

        /// <summary>
        /// 年齢を計算します。
        /// </summary>
        /// <param name="stringbirthDay">誕生日</param>
        /// <returns>年齢</returns>
        public static int AgeCal(string stringbirthDay)
        {
            int age;
            DateTime birthDay = Convert.ToDateTime(stringbirthDay);
            age = Today().Year - birthDay.Year;
            age -= birthDay > Today().AddYears(-age) ? 1 : 0;
            return age;
        }

        /// <summary>
        /// 就学可能な満年齢を計算します。5歳以下は0歳
        /// </summary>
        /// <param name="stringbirthDay">誕生日</param>
        /// <returns>年齢：5歳以下は0歳</returns>
        public static int AgeManCal(string stringbirthDay)
        {
            int age;
            DateTime birthDay = Convert.ToDateTime(stringbirthDay);
            DateTime today = Today().AddDays(1);
            DateTime gradeage = Convert.ToDateTime(today.Year + "-04-01");
            age = gradeage.Year - birthDay.Year;
            if (gradeage.Month < birthDay.Month ||
                (gradeage.Month == birthDay.Month &&
                gradeage.Day < birthDay.Day))
            {
                age--;
            }
            if (age < 6)
            {
                age = 0;
            }
            else if (age > 14)
            {
                age = 15;
            }

            return age;
        }

        /// <summary>
        /// 就学可能満年齢より現在の学年を取得します。
        /// </summary>
        /// <param name="age">就学可能満年齢</param>
        /// <returns>学年</returns>
        public static string GradeCal(int age)
        {
            var grade = _masterdb.age_m.SingleOrDefault(x => x.age == age);
            if (grade != null)
            {
                return grade.divisions_m.name + grade.grade;
            }
            return "計算不能";
        }


        /// <summary>
        /// 今月の最初日取得
        /// </summary>
        /// <returns>Date型 月の１日</returns>
        public static DateTime getFDM()
        {
            DateTime dtFDM = new DateTime(Today().Year, Today().Month, 1);
            return dtFDM;
        }

        /// <summary>
        /// 指定年月の最初日取得
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns>Date型 月の１日</returns>
        public static DateTime getFDM(int year, int month)
        {
            DateTime dtFDM = new DateTime(year, month, 1);
            return dtFDM;
        }

        /// <summary>
        /// 今月の最終日取得
        /// </summary>
        /// <returns>Date型 月の最終日</returns>
        public static DateTime getLDM()
        {
            DateTime dtLDM = new DateTime(Today().Year, Today().Month, DateTime.DaysInMonth(Today().Year, Today().Month));
            return dtLDM;
        }

        /// <summary>
        /// 指定年月の最終日取得
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns>Date型 月の最終日</returns>
        public static DateTime getLDM(int year, int month)
        {
            DateTime dtLDM = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return dtLDM;
        }


        public static string CheckAttendRate(string students_id, int year, int month)
        {
            

            DateTime dtFDM = new DateTime(year, month, 1);
            DateTime dtLDM = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var student_attend = _studentdb.students_attendance.AsEnumerable().Where(x => x.attendance_day >= dtFDM && x.attendance_day <= dtLDM && x.students_id == students_id);
            var month_attend = _masterdb.atteds_m.SingleOrDefault(x => x.year_month == dtFDM);
            

            if (student_attend != null && month_attend != null)
            {
                decimal attend_count = student_attend.Count();
                decimal month_count = month_attend.g0_count;
                decimal rate = attend_count / month_count * 100;
                return rate + "％";
            }

            return "0%";
        }


        /// <summary>
        /// 好き苦手マスタ
        /// </summary>
        public static Dictionary<string, long> likedislike_items = new Dictionary<string, long>{
                { "好き", 1 },
                { "苦手", 2 }
        };

        public static Dictionary<string, string> reports = new Dictionary<string, string>
        {
            { "出席一覧", "StudentMonthlyAttend" },
            { "指導一覧", "StudentMonthlyGuid" },
            { "自立チェック一覧", "StudentMonthlyIndependent" },
            { "月次レポート", "StudentMonthlyReports" }
        };

    }
}