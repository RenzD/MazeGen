
interface IDragonAction
{
    string Name { get; }
    /*
        public int repeatCount;
    */
    bool IsDoing { get; set; }

    bool IsDone();
    void Do();
    bool CanDo();
}
