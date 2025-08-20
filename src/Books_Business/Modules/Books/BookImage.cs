namespace Books_Business.Modules.Books
{
    public class BookImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        //Relationship
        public Book Book { get; set; }
    }
}