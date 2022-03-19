using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElysiumPlayer.AAudio
{
    public enum aaudio_policy_t
    {
        /**
         * Related feature is disabled and never used.
         */
        AAUDIO_POLICY_NEVER = 1,
        /**
         * If related feature works then use it. Otherwise fall back to something else.
         */
        AAUDIO_POLICY_AUTO,
        /**
         * Related feature must be used. If not available then fail.
         */
        AAUDIO_POLICY_ALWAYS
    };
}