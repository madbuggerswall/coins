public class Pair<T> {
	public T first;
	public T second;
	
	public Pair() { }
	public Pair(T first, T second) {
		this.first = first;
		this.second = second;
	}
}

public class Pair<T, U> {
	public T first;
	public U second;
	
	public Pair() { }
	public Pair(T first, U second) {
		this.first = first;
		this.second = second;
	}
}