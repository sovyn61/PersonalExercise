using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class YYMMInput : InputField
{
    private string str_lastValue;


    protected override void Awake()
    {
        onValueChanged.AddListener(OnTextChange);
    }

    public void OnTextChange(string content)
    {
        //
        if(!string.IsNullOrEmpty(str_lastValue)&& str_lastValue.Length > content.Length)
        {
            str_lastValue = content;
            return;
        }

        //
        string[] split = content.Split('/');

        StringBuilder stringBuilder = QString.GetShareStringBuilder();
        if (split.Length > 1)
        {
            for (int i = 0; i < split.Length; ++i)
            {
                stringBuilder.Append(split[i]);
            }
            content = stringBuilder.ToString();
        }

        if (content.Length <= 2)
            return;

        if (int.TryParse(content, out int mainValue))
        {
            if (mainValue == 0)
            {
                str_lastValue = "0";
            }
            else
            {
                //
                int temValue = int.Parse(content.Substring(0, 1));
                while (temValue == 0 && content.Length > 1)
                {
                    content = content.Substring(1);
                    temValue = int.Parse(content.Substring(0, 1));
                }
                if (temValue >= 2 && temValue < 10 && content.Length == 1)
                {
                    stringBuilder = QString.GetShareStringBuilder();
                    stringBuilder.Append("0").Append(content[0]).Append("/");
                    for (int i = 1, j = 0; i < content.Length && j < 2; ++i, ++j)
                    {
                        stringBuilder.Append(content[i]);
                    }
                    str_lastValue = stringBuilder.ToString();
                }
                else
                {
                    temValue = int.Parse(content.Substring(0, 2));
                    if (temValue == 11 || temValue == 12)
                    {
                        stringBuilder = QString.GetShareStringBuilder();
                        stringBuilder.Append(temValue).Append("/");
                        for (int i = 2, j = 0; i < content.Length && j < 2; ++i, ++j)
                        {
                            stringBuilder.Append(content[i]);
                        }
                        str_lastValue = stringBuilder.ToString();
                    }
                    else
                    {
                        stringBuilder = QString.GetShareStringBuilder();
                        stringBuilder.Append("0").Append(content[0]).Append("/");
                        for (int i = 1, j = 0; i < content.Length && j < 2; ++i, ++j)
                        {
                            stringBuilder.Append(content[i]);
                        }
                        str_lastValue = stringBuilder.ToString();
                    }
                }
            }
        }
        else
        {
            str_lastValue = "";
        }
        text = str_lastValue;
        MoveTextEnd(false);
    }
}
