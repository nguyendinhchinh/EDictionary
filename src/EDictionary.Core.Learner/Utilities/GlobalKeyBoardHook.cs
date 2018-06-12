using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Utilities
{
	public struct KeyInfo
	{
		public Keys KeyData;
		public List<Keys> KeysHold;

		public KeyInfo(Keys keyData, List<Keys> keysHold)
		{
			KeyData = keyData;
			KeysHold = keysHold;
		}
	}

	public class GlobalKeyboardHook
	{
		[DllImport("user32.dll")]
		static extern int CallNextHookEx(IntPtr hhk, int code, int wParam, ref KeyBoardHookStruct lParam);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, LLKeyboardHook callback, IntPtr hInstance, uint theardID);

		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);

		// This is the Constructor. This is the code that runs every time you create a new GlobalKeyboardHook object
		public GlobalKeyboardHook()
		{
			llkh = new LLKeyboardHook(HookProc);
			// This starts the hook. You can leave this as comment and you have to start it manually (the thing I do in the tutorial, with the button)
			// Or delete the comment mark and your hook will start automatically when your program starts (because a new GlobalKeyboardHook object is created)
			// That's why there are duplicates, because you start it twice! I'm sorry, I haven't noticed this...
			// hook(); <-- Choose!
		}

		~GlobalKeyboardHook()
		{
			Unhook();
		}

		public delegate int LLKeyboardHook(int Code, int wParam, ref KeyBoardHookStruct lParam);

		public struct KeyBoardHookStruct
		{
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x0100;
		const int WM_KEYUP = 0x0101;
		const int WM_SYSKEYDOWN = 0x0104;
		const int WM_SYSKEYUP = 0x0105;

		LLKeyboardHook llkh;
		public List<Keys> hookedKeys = new List<Keys>();

		IntPtr hook = IntPtr.Zero;

		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;
		public Action<KeyInfo> KeyPressedCallback;

		private List<Keys> keysHold = new List<Keys>();

		public void Hook()
		{
			IntPtr hInstance = LoadLibrary("User32");
			hook = SetWindowsHookEx(WH_KEYBOARD_LL, llkh, hInstance, 0);
		}

		public void Unhook()
		{
			UnhookWindowsHookEx(hook);
		}

		private bool IsModifiers(Keys key)
		{
			foreach (Modifiers modifier in Enum.GetValues(typeof(Modifiers)))
			{
				if (key == (Keys)modifier)
					return true;
			}
			return false;
		}

		public int HookProc(int code, int wParam, ref KeyBoardHookStruct lParam)
		{
			if (code >= 0)
			{
				Keys key = (Keys)lParam.vkCode;

				if (hookedKeys.Contains(key))
				{
					KeyEventArgs kArg = new KeyEventArgs(key);

					if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
					{
						KeyDown?.Invoke(this, kArg);

						if (IsModifiers(key))
							keysHold.Add(key);
					}
					else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
					{
						KeyUp?.Invoke(this, kArg);

						if (IsModifiers(key))
							keysHold.Remove(key);

						KeyPressedCallback.Invoke(new KeyInfo(key, keysHold));
					}

					if (kArg.Handled)
					{
						return 1;
					}
				}
			}
			return CallNextHookEx(hook, code, wParam, ref lParam);
		}
	}
}
