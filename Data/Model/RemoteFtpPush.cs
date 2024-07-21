namespace Data.Model
{
    public class RemoteFtpPush
    {
        public string Remoteftphostname { get; set; }
        public string Remotebookingsfoldername { get; set; }
        public string Remotetrackingfoldername { get; set; }
        public string Remotepodfoldername { get; set; }
        public bool Isremotepushenabled { get; set; }
        public string Remoteftpusername { get; set; }
        public string Remoteftppassword { get; set; }
        public bool SkipFtpAccess { get; set; }

    }

}
