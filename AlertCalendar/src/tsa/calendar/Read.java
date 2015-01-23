package tsa.calendar;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.LinkedList;

public class Read {
	  String content;
	  String events = "";
	  String times = "";
	  String[] contentA;
	  LinkedList<String> eventsA = new LinkedList<String>();
	  LinkedList<String> timesA = new LinkedList<String>();
	public void read(String file){
		try {
			content = new String(Files.readAllBytes(Paths.get(file)));
			
			contentA = (content.split("'"));
			
			//Cycles through the array of content in the calendar file
			//and saves the Event parts of the file into an array
			for(int i = 0; i < contentA.length; i += 2){
				eventsA.add(contentA[i]);
				events = eventsA.toString().replace('[', ' ').replace(']', ' ');
			}
			events = events.trim().replaceAll("\\n|\\r", " ").replaceAll("   ", " ").replaceAll(" ,", ",");
			
			for(int i = 1; i < contentA.length; i += 2){
				timesA.add(contentA[i]);
				times = timesA.toString().replace('[', ' ').replace(']', ' ');
			}
			times = times.trim().replaceAll("  ", " ").replaceAll(" ,", ",");
			
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
