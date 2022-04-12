using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int Level;
    public static string Time;
    public static int DeathCounter = 0;
    public static int ReachedLevel;
    public static bool LoggedIn { get { return username != null; } }
    public static void Logout()
    {
        username = null;
    }
}
