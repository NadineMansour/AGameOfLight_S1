class CreateTeacherRequestSubjects < ActiveRecord::Migration
  def change
    create_table :teacher_request_subjects do |t|
      t.boolean :verified
      t.belongs_to :school_admin
      t.belongs_to :teacher
      t.belongs_to :subject
      t.string :school
      t.timestamps null: false
    end
  end
end
