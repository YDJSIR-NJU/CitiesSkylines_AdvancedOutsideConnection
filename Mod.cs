﻿
//          Copyright Dominic Koepke 2020 - 2020.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          https://www.boost.org/LICENSE_1_0.txt)

using ICities;

namespace AdvancedOutsideConnection
{
    public class Mod : IUserMod
    {
        private static readonly string version = "1.3";

        public static string ModName => "AdvancedOutsideConnection";

        public string Name { get { return ModName; } }

        public string Description => "Advanced options for outside connections.";
    }
}
