﻿namespace CreaturesNCaves.Server.Controllers.Models
{
    public interface ILoginData : IUserData
    {
        string RememberMe { get; set; }
    }
}