﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using GildtAPI.Model;
using System.Net.Http;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using GildtAPI.Controllers;
using GildtAPI.DAO;

namespace GildtAPI.Controllers
{
    class SongRequestController : Singleton<SongRequestController>
    {

        public async Task<List<SongRequest>> GetAllSongrequests()
        {
            return await SongRequestDAO.Instance.GetAllSongrequests();
        }
        public async Task<SongRequest> GetSongrequest(int id)
        {
            return await SongRequestDAO.Instance.GetSongrequest(id);
        }

        public async Task<int> DeleteSongrequest(int id)
        {
            return await SongRequestDAO.Instance.DeleteSongrequest(id);
        }

        public async Task<int> AddSongRequest(SongRequest song)
        {
            return await SongRequestDAO.Instance.AddSongRequest(song);

        }
        public async Task<int> UpVote(int RequestId, int UserId)
        {
            return await SongRequestDAO.Instance.Upvote(RequestId, UserId);
        }
        public async Task<int> Downvote(int RequestId, int UserId)
        {
            return await SongRequestDAO.Instance.Downvote(RequestId, UserId);
        }
      
    }
}
