namespace Core
{
    public enum CONTROLLER_STATE : byte
    {
        STARTING = 0,
        WAITING = 2,
        BUSY = 4,
        INTERRUPT = 8,
        END = 16,
        EXECUTED = 32, 
        READY = 64,
        IDLE = 128

    }
}
