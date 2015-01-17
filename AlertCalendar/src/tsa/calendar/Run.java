package tsa.calendar;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.LinkedList;

public class Run implements Runnable{
	private Thread thread;
	private boolean running = false;
	
	private String events;
	private String times;
	
	private String[] eContent;
	private String[] tContent;
	LinkedList<String> eventsA = new LinkedList<String>();
	LinkedList<String> timesA = new LinkedList<String>();
	
	private Calendar cal = Calendar.getInstance();
	private SimpleDateFormat sdf = new SimpleDateFormat("HH:mm");
	Read read = new Read();
	
	public Run(){
		//Run the read function in the class "Read"
		read.read("src/Events");
		events = read.events;
		times = read.times;
		eContent = events.split(", ");
		for(int i = 0; i < eContent.length; i++){
			eventsA.add(eContent[i]);
		}
		
		tContent = times.split(", ");
		for(int i = 0; i < tContent.length; i++){
			timesA.add(tContent[i]);
		}
		
		start();
	}
	
	public synchronized void start(){
		thread = new Thread(this);
		thread.start();
		running = true;
	}
	
	public synchronized void stop(){
		try{
			thread.join();
			running = false;
		}catch(Exception e){
			e.printStackTrace();
		}
	}
	
	public void run() {
		long lastTime = System.nanoTime();
		double amountOfTicks = 60.0;
		double ns = 1000000000 / amountOfTicks;
		double delta = 0;
		long timer = System.currentTimeMillis();
		while(running){
			long now = System.nanoTime();
			delta += (now - lastTime) / ns;
			lastTime = now;
			while(delta >= 1){
				delta--;
			}
			if(running){
				
				//Checking whether the current time is equal to that
				//of one in the times array
				if(System.currentTimeMillis() - timer > 1000){
					timer += 10000;
					alert();
				}
			}
		}
		stop();
	}
	
	public void alert(){
		for(int i = 0; i < timesA.size(); i++){
			if(sdf.format(cal.getTime()).equals(timesA.get(i))){
				System.out.println("It is " + sdf.format(cal.getTime()) + " and it is time for you to " + eventsA.get(i));
				new AlertOnTime(1000, 100, "AlertNow");
			}
		}
	}
	
	public static void main(String [] args){
		new Run();
	}
}
