﻿namespace WebApiAssignemnt.Dto
{
    public class ReqGetUserListDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
