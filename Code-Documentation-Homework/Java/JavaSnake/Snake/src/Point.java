import java.awt.Color;
import java.awt.Graphics;
import java.awt.Rectangle;
/**
 * Defines Point object
 * @author Zhan
 *
 */
public class Point {
	private int x, y;
	private final int WIDTH = 20;
	private final int HEIGHT = 20;
	
	public Point(Point p) {
		this(p.x, p.y);
	}
	
	public Point(int x, int y ) {
		this.x = x;
		this.y = y;
	}	
		
	public int getX() {
		return x;
	}
	public void setX(int x) {
		this.x = x;
	}
	public int getY() {
		return y;
	}
	public void setY(int y) {
		this.y = y;
	}
	/**
	 * Draws an object on the console
	 * @param g
	 * @param color
	 */
	public void draw(Graphics g, Color color) {
		g.setColor(Color.BLACK);
		g.fillRect(x, y, WIDTH, HEIGHT);
		g.setColor(color);
		g.fillRect(x+1, y+1, WIDTH-2, HEIGHT-2);		
	}
	/**
	 * overrides toString()
	 */
	public String toString() {
		return "[x=" + x + ",y=" + y + "]";
	}
	/**
	 * overrides equals()
	 */
	public boolean equals(Object objects) {
        if (objects instanceof Point) {
            Point point = (Point)objects;
            return (x == point.x) && (y == point.y);
        }
        return false;
    }
}
