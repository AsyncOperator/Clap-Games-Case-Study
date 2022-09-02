public interface ICollectable {
    bool CanCollect { get; set; }
    void Collect();
}