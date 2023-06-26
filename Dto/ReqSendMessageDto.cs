﻿using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace WebApiAssignemnt.Dto
{
    public class ReqSendMessageDto
    {
        [Required]
        public int receiverId { get; set; }
        [Required]
        public string MessageContent { get; set; }         

    }
}
