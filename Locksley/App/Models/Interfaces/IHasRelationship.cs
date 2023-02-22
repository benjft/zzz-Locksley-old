using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Models.Interfaces; 

public interface IHasRelationship {
    static abstract void ConfigureRelationships(ModelBuilder modelBuilder);
}