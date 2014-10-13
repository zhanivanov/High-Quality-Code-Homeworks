import java.awt.Color;
import java.awt.Graphics;
import java.util.LinkedList;
/**
 * Defines Snake object
 * @author Zhan
 *
 */
public class Snake{
	LinkedList<Point> snakeBody = new LinkedList<Point>();
	private Color snakeColor;
	private int velocityX, velocityY;
	
	public Snake() {
		snakeColor = Color.GREEN;
		snakeBody.add(new Point(300, 100)); 
		snakeBody.add(new Point(280, 100)); 
		snakeBody.add(new Point(260, 100)); 
		snakeBody.add(new Point(240, 100)); 
		snakeBody.add(new Point(220, 100)); 
		snakeBody.add(new Point(200, 100)); 
		snakeBody.add(new Point(180, 100));
		snakeBody.add(new Point(160, 100));
		snakeBody.add(new Point(140, 100));
		snakeBody.add(new Point(120, 100));

		velocityX = 20;
		velocityY = 0;
	}
	/**
	 * Draws all points from the snakeBody list
	 * @param g
	 */
	public void drawSnake(Graphics g) {		
		for (Point point : this.snakeBody) {
			point.draw(g, snakeColor);
		}
	}
	/**
	 * Implements snake move ticks
	 */
	public void tick() {
		Point newSnakePointPosition = new Point((snakeBody.get(0).getX() + velocityX), (snakeBody.get(0).getY() + velocityY));
		
		if (newSnakePointPosition.getX() > Game.WIDTH - 20) {
		 	Game.gameRunning = false;
		} else if (newSnakePointPosition.getX() < 0) {
			Game.gameRunning = false;
		} else if (newSnakePointPosition.getY() < 0) {
			Game.gameRunning = false;
		} else if (newSnakePointPosition.getY() > Game.HEIGHT - 20) {
			Game.gameRunning = false;
		} else if (Game.myApple.getPoint().equals(newSnakePointPosition)) {
			snakeBody.add(Game.myApple.getPoint());
			Game.myApple = new Apple(this);
			Game.points += 50;
		} else if (snakeBody.contains(newSnakePointPosition)) {
			Game.gameRunning = false;
			System.out.println("You ate yourself");
		}	
		
		for (int i = snakeBody.size()-1; i > 0; i--) {
			snakeBody.set(i, new Point(snakeBody.get(i-1)));
		}	
		snakeBody.set(0, newSnakePointPosition);
	}

	public int getVelX() {
		return velocityX;
	}

	public void setVelX(int velX) {
		this.velocityX = velX;
	}

	public int getVelY() {
		return velocityY;
	}

	public void setVelY(int velY) {
		this.velocityY = velY;
	}
}
