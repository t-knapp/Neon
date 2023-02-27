namespace Neon.Domain;

public abstract class Entity {
    public Guid Id { get; init; }

    protected Entity() { }

    public Entity( Guid id ) {
        Id = id;
    }
}