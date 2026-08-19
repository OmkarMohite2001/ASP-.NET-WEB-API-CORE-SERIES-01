namespace ComponyRegistrationAPI.Models
{
    public class Compony
    {
        public int Id { get; set; }
        public string? ComponyName { get; set; }
        public string? Email   { get; set; }        
        public string? Website {  get; set; }

        // 1    :  1
        public ComponyRegistration? Registration { get; set; }  //Navigation Property
        // 1 : M
        public ICollection<ComponyBranch> Branches { get; set; }= new List<ComponyBranch>();

        // M to M
        public ICollection<Service> services { get; set; } = new List<Service>();
    }
}
