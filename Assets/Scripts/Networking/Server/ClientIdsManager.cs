﻿using System;
using System.Collections.Generic;

namespace StupidNetworking
{
    public static class ClientIdsManager
    {
        public const byte MAX_PLAYERS = 253;
        public const byte SERVER_CLIENT_ID = 254;

        private static List<byte> ids = new List<byte>();

        public static byte CreateId()
        {
            if (ids.Count >= MAX_PLAYERS)
                throw new Exception("Maximum clients number reached.");

            for (byte i = 0; i < MAX_PLAYERS; i++)
            {
                if (!ids.Contains(i))
                {
                    ids.Add(i);
                    return i;
                }
            }

            throw new Exception("[IdsManager] No free Id available. Too many players.");
        }

        public static void FreeId(byte id)
        {
            ids.Remove(id);
        }
    }
}