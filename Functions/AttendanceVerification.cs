using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GildtAPI.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using GildtAPI.Controllers;

namespace GildtAPI.Functions
{
    public static class AttendanceVerification
    {
        [FunctionName(nameof(AttendanceVerification) + "-" + nameof(GetVerifications))]
        public static async Task<IActionResult> GetVerifications([HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "Attendance/{eventId?}")] HttpRequest req, ILogger log,
            int? eventId)
        {
            log.LogInformation("C# HTTP trigger function processed a request: " + nameof(GetVerifications));

            string qCount = req.Query["count"];
            int count;
            if (qCount != null)
            {
                Int32.TryParse(qCount, out count);
                if (count < 1)
                {
                    return new BadRequestObjectResult("Invalid count. Count must be 1 or higher.");
                }
            }
            else
            {
                count = Constants.DEFAULTCOUNT;
            }

            List<Attendance> attendanceList = await AttendanceController.Instance.GetAttendanceList(eventId, count);
            if (attendanceList.Count == 0)
            {
                return new BadRequestObjectResult($"No verifications found for event with id = {eventId}");
            }
            string jAttendance = JsonConvert.SerializeObject(attendanceList.ToArray());
            return new OkObjectResult(jAttendance);
        }

        [FunctionName(nameof(AttendanceVerification) + "-" + nameof(GetUserVerifications))]
        public static async Task<IActionResult> GetUserVerifications([HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "User/{userId}/Attendance/")] HttpRequest req, ILogger log,
            int userId)
        
{
            log.LogInformation("C# HTTP trigger function processed a request: " + nameof(GetUserVerifications));

            string qCount = req.Query["count"];
            int count;
            if (qCount != null)
            {
                Int32.TryParse(qCount, out count);
                if (count < 1)
                {
                    return new BadRequestObjectResult("Invalid count. Count must be 1 or higher.");
                }
            }
            else
            {
                count = Constants.DEFAULTCOUNT;
            }

            List<Attendance> attendanceList = await AttendanceController.Instance.GetUserAttendanceList(userId, count);
            string jAttendance = JsonConvert.SerializeObject(attendanceList.ToArray());
            if (attendanceList.Count == 0)
            {
                return new OkObjectResult($"No verifications found for user #{userId}");
            }
            return new OkObjectResult(jAttendance);
        }

        [FunctionName(nameof(AttendanceVerification) + "-" + nameof(AddVerification))]
        public static async Task<IActionResult> AddVerification([HttpTrigger(AuthorizationLevel.Anonymous, "post",
            Route = "Attendance/{eventId}/AddVerification/{userId}")] HttpRequest req, ILogger log,
            int userId, int eventId)
        {
            //check if verification exists
            if (await AttendanceController.Instance.CheckVerification(userId, eventId))
            {
                return new BadRequestObjectResult($"Verification already exists for user #{userId} at event #{eventId}");
            }
            else
            {
                try
                {
                    await AttendanceController.Instance.CreateVerification(userId, eventId);
                }
                catch
                {
                    return new BadRequestObjectResult($"Something went wrong while trying to add verification for user #{userId} at event {eventId}");
                }

                return new OkObjectResult($"Verification succesfully created for {userId} at event {eventId}");
            }
        }

        [FunctionName(nameof(AttendanceVerification) + "-" + nameof(DeleteVerification))]
        public static async Task<IActionResult> DeleteVerification([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
            Route = "Attendance/{eventId}/Delete/{userId}")] HttpRequest req, ILogger log,
            int userId, int eventId)
        {
            log.LogInformation($"C# HTTP trigger function processed a request: {nameof(DeleteVerification)}");
            if (eventId < 1 || userId < 1)
            {
                return new BadRequestObjectResult("Invalid parameters.");
            }
            int affectedRows;
            try
            {
                affectedRows = await AttendanceController.Instance.DeleteVerification(userId, eventId);
            }
            catch
            {
                affectedRows = -1;
            }
            switch (affectedRows)
            {
                case 1:
                    return new OkObjectResult($"Successfully deleted the verification for user#{userId} at event #{eventId}");
                case 0:
                    return new BadRequestObjectResult($"Deleting verification failed: verification with UserId {userId} " +
                        $"and EventId {eventId} does not exist!");
                case -1:
                    return new BadRequestObjectResult($"SQL query failed: delete verification for user#{userId} at event#{eventId}");
                default:
                    return new OkObjectResult("Deleted the verification. Duplicate verification(s) found and also deleted.");
            }
        }
    }
}
