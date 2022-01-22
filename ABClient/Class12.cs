using System;
using System.Collections.Generic;

// Token: 0x02000045 RID: 69
internal static class Class12
{
	// Token: 0x0600012A RID: 298 RVA: 0x00017C54 File Offset: 0x00015E54
	internal static string smethod_0(string string_0, string string_1, string string_2, string string_3)
	{
		int num = string_0.IndexOf(string_1, StringComparison.OrdinalIgnoreCase);
		if (num == -1)
		{
			return string_0;
		}
		int num2 = string_0.IndexOf(string_2, num + string_1.Length, StringComparison.OrdinalIgnoreCase);
		if (num2 != -1)
		{
			return string_0.Substring(0, num + string_1.Length) + string_3 + string_0.Substring(num2);
		}
		return string_0;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00017CA4 File Offset: 0x00015EA4
	internal static string smethod_1(string string_0, string string_1, string string_2)
	{
		int num = string_0.IndexOf(string_1, StringComparison.OrdinalIgnoreCase);
		if (num == -1)
		{
			return null;
		}
		int num2 = string_0.IndexOf(string_2, num + string_1.Length, StringComparison.OrdinalIgnoreCase);
		if (num2 != -1)
		{
			return string_0.Substring(num + string_1.Length, num2 - num - string_1.Length);
		}
		return null;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00017CF0 File Offset: 0x00015EF0
	internal static string[] smethod_2(string string_0)
	{
		List<string> list = new List<string>();
		int num = 0;
		do
		{
			int num2 = num;
			if (string_0[num2] == '\'')
			{
				int num3 = string_0.IndexOf('\'', num2 + 1);
				if (num3 == -1)
				{
					break;
				}
				string item = string_0.Substring(num2 + 1, num3 - num2 - 1);
				list.Add(item);
				num = num3 + 1;
				if (num < string_0.Length)
				{
					if (string_0[num] != ',')
					{
						break;
					}
					num++;
				}
			}
			else
			{
				int num4 = string_0.IndexOf(',', num2 + 1);
				if (num4 == -1)
				{
					num4 = string_0.Length;
				}
				string item2 = string_0.Substring(num2, num4 - num2);
				list.Add(item2);
				num = num4 + 1;
			}
		}
		while (num < string_0.Length);
		return list.ToArray();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00017DA0 File Offset: 0x00015FA0
	/*internal static string[] smethod_3(string string_0)
	{
		string[] array = string_0.Split(new string[]
		{
			Environment.NewLine
		}, StringSplitOptions.RemoveEmptyEntries);
		List<string> list = new List<string>();
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]) && array[i][0] != ';')
			{
				list.Add(array[i]);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		List<string> list2 = new List<string>();
		while (list.Count > 0)
		{
			int index = Class89.smethod_0(list.Count);
			list2.Add(list[index]);
			list.RemoveAt(index);
		}
		return list2.ToArray();
	}*/

	// Token: 0x0600012E RID: 302 RVA: 0x00017E3C File Offset: 0x0001603C
	internal static List<List<string>> smethod_4(string string_0)
	{
		if (string.IsNullOrEmpty(string_0) || string_0.Length < 2)
		{
			return null;
		}
		List<List<string>> list = new List<List<string>>();
		int num;
		for (int i = 0; i < string_0.Length; i = num + 1)
		{
			if (string_0[i] != '[')
			{
				num = string_0.IndexOf(',', i + 1);
			}
			else
			{
				num = string_0.IndexOf(']', i + 1);
				if (num != -1)
				{
					num++;
				}
			}
			if (num == -1)
			{
				num = string_0.Length;
			}
			string text = string_0.Substring(i, num - i);
			List<string> list2 = new List<string>();
			if (text.Length > 0)
			{
				if (text[0] != '[')
				{
					text = text.Trim(new char[]
					{
						' ',
						'"',
						'\''
					});
					list2.Add(text);
				}
				else
				{
					text = text.Trim(new char[]
					{
						' ',
						'[',
						']'
					});
					string[] array = text.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array.Length; j++)
					{
						string item = array[j].Trim(new char[]
						{
							' ',
							'"',
							'\''
						});
						list2.Add(item);
					}
				}
			}
			list.Add(list2);
		}
		return list;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00017F6C File Offset: 0x0001616C
	/*internal static bool smethod_5()
	{
		int int_ = Class72.int_14;
		int int_2 = Class72.int_15;
		int num = (Class72.class19_0.timeSpan_0 == TimeSpan.MinValue) ? DateTime.Now.Hour : DateTime.Now.Subtract(Class72.class19_0.timeSpan_0).Hour;
		bool flag = int_ > int_2;
		return (flag && (num >= int_ || num < int_2)) || (!flag && num >= int_ && num < int_2);
	}*/
}
