using System;
using System.Collections.Generic;

namespace BookStoreAppAPI.Data;

public partial class Author
{
    public /*Guid*/ int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
