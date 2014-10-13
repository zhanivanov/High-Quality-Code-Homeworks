import java.applet.Applet;
import java.awt.Dimension;
import java.awt.Graphics;

/**
 * Implements the game field and adds Game objects in it
 * @author Zhan
 *
 */
@SuppressWarnings("serial")
public class GameApplet extends Applet {
	private Game game;
	GetPressedButton IH;
	
	public void init(){
		game = new Game();
		game.setPreferredSize(new Dimension(800, 650));
		game.setVisible(true);
		game.setFocusable(true);
		this.add(game);
		this.setVisible(true);
		this.setSize(new Dimension(800, 650));
		IH = new GetPressedButton(game);
	}
	
	public void paint(Graphics g){
		this.setSize(new Dimension(800, 650));
	}

}
