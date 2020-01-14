using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GildtAPI.DAO;
using GildtAPI.Model;

namespace GildtAPI.Controllers
{
    class AttendanceController : Singleton<AttendanceController>
    {
        public async Task<bool> CheckVerification(int userId, int eventId)
        {
            return await AttendanceDAO.Instance.CheckVerification(userId, eventId);
        }

        public async Task CreateVerification(int userId, int eventId)
        {
            await AttendanceDAO.Instance.CreateVerification(userId, eventId);
        }

        public async Task<List<Attendance>> GetUserAttendanceList(int userId, int count)
        {
            return await AttendanceDAO.Instance.GetUserAttendanceList(userId, count);
        }

        public async Task<List<Attendance>> GetAttendanceList(int? eventId, int count)
        {
            return await AttendanceDAO.Instance.GetAttendanceList(eventId, count);
        }

        public async Task<int> DeleteVerification(int userId, int eventId)
        {
            return await AttendanceDAO.Instance.DeleteVerification(userId, eventId);
        }
    }
}
