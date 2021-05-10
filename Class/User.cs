using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Class
{
    class User
    {
        private int userid;
        private string useridstring;

        public User(int userid)
        {
            this.userid = userid;
        }


        public int getUserid()
        {
            return this.userid;
        }

        //public void setUserid(int userid)
        //{
        //    this.userid = userid;
        //}

    }
}
