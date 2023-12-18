using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace DAL
{
    public class Mailer
    {
        private string _AUTH_HOST = "mail.tiffaedi.com";
        public string AUTH_HOST
        {
            get { return _AUTH_HOST; }
            set { _AUTH_HOST = value; }
        }
        private int _AUTH_PORT = 25;
        public int AUTH_PORT
        {
            get { return _AUTH_PORT; }
            set { _AUTH_PORT = value; }
        }
        private string _AUTH_USER = "apilak";
        public string AUTH_USER
        {
            get { return _AUTH_USER; }
            set { _AUTH_USER = value; }
        }
        private string _AUTH_PASS = "qwertyui";
        public string AUTH_PASS
        {
            get { return _AUTH_PASS; }
            set { _AUTH_PASS = value; }
        }

        private int _TimeOut = 0;
        public int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }

        private List<System.IO.FileInfo> _FileAttachment = new List<System.IO.FileInfo>();
        public List<System.IO.FileInfo> FileAttachment
        {
            get { return _FileAttachment; }
            set { _FileAttachment = value; }
        }

        private string _Subject = string.Empty;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        private string _Body = string.Empty;
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        //private System.Net.Mail.MailAddress _Sender;
        //public System.Net.Mail.MailAddress Sender
        //{
        //    get { return _Sender; }
        //    set { _Sender = value; }
        //}
        private System.Net.Mail.MailAddress _From;
        public System.Net.Mail.MailAddress From
        {
            get { return _From; }
            set { _From = value; }
        }
        private List<System.Net.Mail.MailAddress> _To = new List<System.Net.Mail.MailAddress>();
        public List<System.Net.Mail.MailAddress> To
        {
            get { return _To; }
            set { _To = value; }
        }
        private List<System.Net.Mail.MailAddress> _CC = new List<System.Net.Mail.MailAddress>();
        public List<System.Net.Mail.MailAddress> CC
        {
            get { return _CC; }
            set { _CC = value; }
        }
        private List<System.Net.Mail.MailAddress> _BCC = new List<System.Net.Mail.MailAddress>();
        public List<System.Net.Mail.MailAddress> BCC
        {
            get { return _BCC; }
            set { _BCC = value; }
        }
        private bool _IsHtml = false;
        public bool IsHtml
        {
            get { return _IsHtml; }
            set { _IsHtml = value; }
        }
        
        public Mailer() { }
        public Mailer(string HOST) { this._AUTH_HOST = HOST; }
        public Mailer(string HOST, int PORT) { this._AUTH_HOST = HOST; this._AUTH_PORT = PORT; }
        public Mailer(string HOST, string User, string Password) { this._AUTH_HOST = HOST; this._AUTH_USER = User; this._AUTH_PASS = Password; }


        public ReturnValue SendMail()
        {
            ReturnValue retVal = new ReturnValue();

            MailMessage _MailMessage = new MailMessage();
            //if (_Sender == null)
            //    return new ReturnValue(false, new Exception("Sender not config."));
            //_MailMessage.Sender = _Sender;

            if (_From == null)
                return new ReturnValue(false, new Exception("Mail From not config."));
            _MailMessage.From = _From;

            if (string.IsNullOrEmpty(_Subject))
                return new ReturnValue(false, new Exception("Subject not config."));
            _MailMessage.Subject = _Subject;
            _MailMessage.SubjectEncoding = Encoding.UTF8;

            if (string.IsNullOrEmpty(_Body))
                return new ReturnValue(false, new Exception("Mail body not config."));
            _MailMessage.Body = _Body;

            _MailMessage.IsBodyHtml = _IsHtml;
            _MailMessage.BodyEncoding = Encoding.UTF8;

            if (_To.Count == 0)
                return new ReturnValue(false, new Exception("MailAddress TO not config."));
            foreach (MailAddress _iTO in _To)
                _MailMessage.To.Add(_iTO);

            foreach (MailAddress _iCC in _CC)
                _MailMessage.CC.Add(_iCC);

            foreach (MailAddress _iBCC in _BCC)
                _MailMessage.Bcc.Add(_iBCC);

            foreach (System.IO.FileInfo _iFileAtt in _FileAttachment)
            {
                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(_iFileAtt.FullName
                    , MediaTypeNames.Application.Octet);
                
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(_iFileAtt.FullName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(_iFileAtt.FullName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(_iFileAtt.FullName);

                // Add the file attachment to this e-mail message.
                _MailMessage.Attachments.Add(data);
            }


            try
            {
                SmtpClient _smtpClient = new SmtpClient();
                _smtpClient.Host = _AUTH_HOST;
                _smtpClient.Port = _AUTH_PORT;

                if (_TimeOut > 0)
                    _smtpClient.Timeout = _TimeOut;

                _smtpClient.Credentials = new NetworkCredential(_AUTH_USER, _AUTH_PASS);
                _smtpClient.Send(_MailMessage);
                _MailMessage.Dispose();
                _MailMessage = null;

                retVal.Value = true;
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }

            return retVal;
        }
    }
}