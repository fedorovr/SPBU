namespace World
{
    interface IGod
    {
        Human CreateHuman();
        Human CreateHuman(Sex sex);
        Human CreatePair(Human human);
    }
}
