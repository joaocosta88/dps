namespace DPS.Common;
public static class Utils {
	public static Stream GenerateStreamFromString(string s)
	{
		 MemoryStream stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(s);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}
}
