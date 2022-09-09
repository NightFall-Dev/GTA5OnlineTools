namespace GTA5OnlineTools.Features.Core;

public class HotKeys
{
    // Keys holder 按键持有者
    private Dictionary<int, MyKeys> keys;

    // Update thread 更新线程
    private readonly int interval = 20; // 20 ms

    // Keys events 按键事件
    public delegate void KeyHandler(int Id, string Name);
    public event KeyHandler KeyUpEvent;
    public event KeyHandler KeyDownEvent;

    private bool isRun = true;

    /// <summary>
    /// Init 初始化
    /// </summary>
    public HotKeys()
    {
        keys = new Dictionary<int, MyKeys>();
        var thread = new Thread(Update);
        thread.IsBackground = true;
        thread.Start();
    }

    /// <summary>
    /// 释放按键监听线程
    /// </summary>
    public void Dispose()
    {
        isRun = false;
    }

    /// <summary>
    /// Key Up 键弹起
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    protected void OnKeyUp(int Id, string Name)
    {
        if (KeyUpEvent != null)
        {
            KeyUpEvent(Id, Name);
        }
    }

    /// <summary>
    /// Key Down 键按下
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    protected void OnKeyDown(int Id, string Name)
    {
        if (KeyDownEvent != null)
        {
            KeyDownEvent(Id, Name);
        }
    }

    /// <summary>
    /// Add key 增加键
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="keyName"></param>
    public void AddKey(int keyId, string keyName)
    {
        if (!keys.ContainsKey(keyId))
        {
            keys.Add(keyId, new MyKeys(keyId, keyName));
        }
    }

    /// <summary>
    /// Add key 增加键
    /// </summary>
    /// <param name="key"></param>
    public void AddKey(WinVK key)
    {
        int keyId = (int)key;
        if (!keys.ContainsKey(keyId))
        {
            keys.Add(keyId, new MyKeys(keyId, key.ToString()));
        }
    }

    /// <summary>
    /// Is Key Down 键是否按下
    /// </summary>
    /// <param name="keyId"></param>
    /// <returns></returns>
    public bool IsKeyDown(int keyId)
    {
        if (keys.TryGetValue(keyId, out MyKeys value))
        {
            return value.IsKeyDown;
        }
        return false;
    }

    /// <summary>
    /// Update Thread 更新线程
    /// </summary>
    /// <param name="sender"></param>
    private void Update(object sender)
    {
        while (isRun)
        {
            if (keys.Count > 0)
            {
                List<MyKeys> keysData = new List<MyKeys>(keys.Values);
                if (keysData != null && keysData.Count > 0)
                {
                    foreach (MyKeys key in keysData)
                    {
                        if (Convert.ToBoolean(WinAPI.GetKeyState(key.Id) & WinAPI.KEY_PRESSED))
                        {
                            if (!key.IsKeyDown)
                            {
                                key.IsKeyDown = true;
                                OnKeyDown(key.Id, key.Name);
                            }
                        }
                        else
                        {
                            if (key.IsKeyDown)
                            {
                                key.IsKeyDown = false;
                                OnKeyUp(key.Id, key.Name);
                            }
                        }
                    }
                }
            }

            Thread.Sleep(interval);
        }
    }
}

public class MyKeys
{
    private string keyName;
    private int keyId;
    private bool keyDown;

    public MyKeys(int keyId, string keyName)
    {
        this.keyId = keyId;
        this.keyName = keyName;
    }

    public string Name
    {
        get { return keyName; }
    }

    public int Id
    {
        get { return keyId; }
    }

    public bool IsKeyDown
    {
        get { return keyDown; }
        set { keyDown = value; }
    }
}
