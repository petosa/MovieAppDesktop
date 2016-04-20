import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.ValueEventListener;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

import java.text.DateFormat;
import java.text.SimpleDateFormat;

import java.util.Date;
import java.util.concurrent.atomic.AtomicBoolean;

public class DBHelper {

    private static final Firebase ref =
            new Firebase("https://movierecommender.firebaseio.com/");

    private static final Firebase USERS_TABLE = ref.child("users");
    private static final Firebase MOVIE_TABLE = ref.child("movies");

    private static PrintWriter USERS_PW;
    private static PrintWriter MOVIES_PW;

    private static final File LOG = new File("log.txt");
    private static FileWriter LOG_FW;
    private static BufferedWriter LOG_BW;
    private static PrintWriter LOG_PW;
    private static final DateFormat LOG_DATE_FORMAT =
            new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");

    private static final AtomicBoolean waiter = new AtomicBoolean();

    public static void main(String[] args) {
        // Initialize logging
        try {
            LOG_FW = new FileWriter(LOG, true);
            LOG_BW = new BufferedWriter(LOG_FW);
            LOG_PW = new PrintWriter(LOG_BW);
            LOG_PW.println("\n\nBegin logging");

            MOVIES_PW = new PrintWriter("movies.csv");
        } catch (IOException e) {
            e.printStackTrace();
        }

        // Write all users to USERS_FILE and update them in realtime
        USERS_TABLE.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                for (DataSnapshot postSnapshot : dataSnapshot.getChildren()) {
                    try {
                        USERS_PW = new PrintWriter("users.csv");
                    } catch (IOException e) {
                        e.printStackTrace();
                    }

                    String name = postSnapshot.child("name").getValue(String.class);
                    String email = postSnapshot.child("email").getValue(String.class);
                    String username = postSnapshot.child("username").getValue(String.class);
                    int passwordHash = postSnapshot.child("passwordHash").getValue(Integer.class);
                    String status = postSnapshot.child("status").getValue(String.class);
                    String major = postSnapshot.child("major").getValue(String.class);
                    String description = postSnapshot.child("description").getValue(String.class);

                    System.out.println("begin printing");

                    USERS_PW.print("@s(");
                    USERS_PW.print(name);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(email);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(username);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(passwordHash);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(status);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(major);
                    USERS_PW.print(");|");
                    USERS_PW.print("@s(");
                    USERS_PW.print(description);
                    USERS_PW.println(");|");
                }

                USERS_PW.close();
            }

            @Override
            public void onCancelled(FirebaseError firebaseError) {
                Date date = new Date();
                StringBuffer temp = new StringBuffer();
                temp.append(LOG_DATE_FORMAT.format(date));
                temp.append("   Error initializing user: ");
                temp.append(firebaseError.getMessage());
                LOG_PW.println(temp);
                LOG_PW.close();
            }
        });

        while (!waiter.get()) {
            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
