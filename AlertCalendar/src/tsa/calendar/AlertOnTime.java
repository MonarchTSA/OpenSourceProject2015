package tsa.calendar;

import java.awt.Canvas;
import java.awt.Dimension;

import javax.swing.JFrame;

public class AlertOnTime extends Canvas{

	private static final long serialVersionUID = 1L;

	public AlertOnTime(int width, int height, String title){
		JFrame frame = new JFrame(title);
	
		frame.setPreferredSize(new Dimension(width, height));
		frame.setMaximumSize(new Dimension(width, height));
		frame.setMinimumSize(new Dimension(width, height));
		
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setResizable(false);
		frame.setLocation(100, 0);
		frame.setVisible(true);
	}
}