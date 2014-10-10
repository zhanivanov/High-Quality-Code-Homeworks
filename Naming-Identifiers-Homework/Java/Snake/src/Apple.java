import java.awt.Color;
import java.awt.Graphics;
import java.util.Random;


public class Apple {
	public static Random randomNumberGenerator;
	private Point AppleObject;
	private Color snakeColor;
	
	public Apple(Snake s) {
		AppleObject = createApple(s);
		snakeColor = Color.RED;		
	}
	
	private Point createApple(Snake s) {
		randomNumberGenerator = new Random();
		int x = randomNumberGenerator.nextInt(30) * 20; 
		int y = randomNumberGenerator.nextInt(30) * 20;
		for (Point snakePoint : s.snakeBody) {
			if (x == snakePoint.getX() || y == snakePoint.getY()) {
				return createApple(s);				
			}
		}
		return new Point(x, y);
	}
	
	public void drawApple(Graphics g) {
		AppleObject.draw(g, snakeColor);
	}	
	
	public Point getPoint() {
		return AppleObject;
	}
}
