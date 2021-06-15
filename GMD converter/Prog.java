package pkg;

import java.io.IOException;
import java.nio.*;
import java.nio.channels.*;
import java.nio.file.*;

public class Prog {
		
	public static void main(String[] args) {
		String S;
		ByteBuffer inbuff = ByteBuffer.allocate(64);
		FileChannel in_chan;
		FileChannel out_chan;
		short NTracks = 0;
		short Divi = 0;
		int chunklen = 0;
		
		S = "funeral"; // filename!
		
		try {
			in_chan = FileChannel.open(Paths.get(S + ".gmd"), StandardOpenOption.READ);
			
			inbuff.limit(8);
			in_chan.read(inbuff);	// GMD header
			inbuff.clear();
			
			inbuff.limit(8);
			in_chan.read(inbuff);	// MDpg header
			chunklen =  inbuff.getInt(4); 			
			inbuff.rewind();
			
			in_chan.position(16 + chunklen);
			in_chan.read(inbuff);	// MThd header
			chunklen = inbuff.getInt(4);
						
			inbuff.clear();
			inbuff.limit(chunklen);
			in_chan.read(inbuff); 	// read MThd contents
			inbuff.position(2); 
			NTracks = inbuff.getShort();
			Divi = inbuff.getShort();
			
			int i;
			for (i=1; i <= NTracks; i++) {		// read tracks and write to new midi file
				inbuff.clear();
				inbuff.limit(8);
				in_chan.read(inbuff);	// read MTrk header
				chunklen = inbuff.getInt(4);
				System.out.print(chunklen + "\n");
				
				ByteBuffer trackbuff = ByteBuffer.allocate(chunklen);
				in_chan.read(trackbuff);	// read MTrk contents
				trackbuff.flip();
				
				/* --------------------------------------------------------------------------------- */
				out_chan = FileChannel.open(Paths.get(S + i + ".mid"), StandardOpenOption.CREATE, StandardOpenOption.WRITE);
				ByteBuffer outbuff = ByteBuffer.allocate(64);
				outbuff.limit(14);
				outbuff.putInt(0x4d546864); 	// "MThd"
				outbuff.putInt(6);
				outbuff.putShort((short) 0);	//	MIDI type 0
				outbuff.putShort((short) 1);		// 1 track
				outbuff.putShort(Divi);
				outbuff.rewind();
				out_chan.write(outbuff);
				
				outbuff.limit(8);
				outbuff.rewind();
				outbuff.putInt(0x4d54726b); 	// "MTrk"
				outbuff.putInt(chunklen);
				outbuff.flip();
				out_chan.write(outbuff);		// write track header
				out_chan.write(trackbuff);		// write track contents
				out_chan.close();
				/* --------------------------------------------------------------------------------- */
			}
			
			in_chan.close();
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	
	}
	
}
