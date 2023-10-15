namespace RingLibrary
{
	public interface IRing<T>
	{
		int Length { get; }
		T Read();
		void InputFromConsole();

	}
}